namespace AppRunner.Services.Application
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    
    using AppRunner.Data;
    using AppRunner.Data.Models;
    using AppRunner.Common.Enums;

    public class ApplicationsService : IApplicationsService
    {
        private readonly ApplicationDbContext context;

        public ApplicationsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(string name, string path, AppType appType)
        {
            Application application = new Application()
            {
                Name = name,
                Path = path,
                Type = appType
            };

            await this.context.Applications.AddAsync(application);
            await this.context.SaveChangesAsync();
        }

        public async Task<Application[]> GetApplicationsAsync()
        {
            return await this.context.Applications.ToArrayAsync();
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await this.context.Applications
                .FirstOrDefaultAsync(app => app.Name == name); 
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !(await this.context.Applications.AnyAsync(app => app.Name == name));
        }

        public async Task<bool> RemoveAsync(string name)
        {
            Application application = await this.GetByNameAsync(name);

            if(application == null)
            {
                return false;
            }
            
            this.context.Remove(application);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateNameAsync(string oldName, string newName)
        {
            Application application = await this.GetByNameAsync(oldName);
            if(application == null)
            {
                return false;
            }

            application.Name = newName;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
