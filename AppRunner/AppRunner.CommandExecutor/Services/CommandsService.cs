namespace AppRunner.CommandExecutor.Services
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AppRunner.CommandExecutor.Commands;
    using AppRunner.Services.Application;

    public static class CommandsService
    {
        public static ICommand[] GetCommandPatterns(IApplicationsService applicationsService)
        {
            Type[] commandTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsAbstract == false && type.IsAssignableTo(typeof(ICommand)))
                .ToArray();

            ICommand[] commands = new ICommand[commandTypes.Length];

            int index = 0;
            foreach(Type commandType in commandTypes)
            {
                ConstructorInfo constructor = GetConstructor(commandType);
                ICommand command = InvokeConstructor(constructor, applicationsService); 
                commands[index++] = command;
            }

            return commands;
        }

        private static ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(); 
            ConstructorInfo emptyConstructor = constructors
                .FirstOrDefault(ctor => ctor.GetParameters().Length == 0);

            if(emptyConstructor != null)
            {
                return emptyConstructor;
            }

            return constructors[0];
        }

        private static ICommand InvokeConstructor(ConstructorInfo constructor, IApplicationsService applicationsService)
        {
            if(constructor.GetParameters().Length == 0)
            {
                return (ICommand)constructor.Invoke(null);
            }

            return (ICommand)constructor.Invoke(new object[] { applicationsService });
        }
    }
}
