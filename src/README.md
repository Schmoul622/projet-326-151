# InterfaceGestionArtiste

Ce projet est une bibliothèque d'artiste musical ou l'on peut ajouter, modifier, supprimer et visualiser des artistes, des albums et des musiques.

## Build

Lancer la commande `dotnet restore` et ensuite la commande `dotnet build`.

## Development server

Ce rendre dans le dossier src et sensuite lancer la commande `dotnet run`. Et ce rendre sur `https://localhost:5001/swagger/index.html` pour voir l'interface swagger.

## Run test

Ce rendre dans le dossier tests et ensuite lancer la commande `dotnet test` pour executer les tests unitaires.

## Infos supplémentaire

- La base de données posséde déjà des données dans toutes les tables.

- Lors de la création d'un artiste ou d'un album il faut saisir la valeur null sous la propriété Picture. Pour ajouter une image ensuite à un artiste par exemple il faudra le faire depuis la
méthode SetPicture.