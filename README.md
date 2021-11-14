# ServerBerry

## Project startup
1. **SecretsVault.WebApi**
    - Arguments (DatabaseType, ConnectionString)
    - Url (http://localhost:5000)

2. **AppRunner.WebApi**
    - Arguments (SecretKey, Environment)
    - Url (http://localhost:5555)
    
To successfully run the ServerBerry project, you first need to run the **SecretsVault API** project. Then you can start your **AppRunner API** and then start all other processes.

## Important
Ports **5000** and **5555** must be free and open, otherwise, you will need to configure SecretsVault API and AppRunner API yourself and then build and run them.