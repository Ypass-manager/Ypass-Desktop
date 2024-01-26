# Ypass-Desktop
Logiciel de gestionnaire de mots de passes en DOTNET 


## Outils pour les développeurs


### Entity Framework
#### Migrations

Ces étapes ne sont nécessaire que lorsque vous voulez faire évoluer la base de données

- **Créer une nouvelle migration** : dotnet ef migrations add NomDeMaMigration
- **Appliquer les migrations sur la base de données** : dotnet ef database update