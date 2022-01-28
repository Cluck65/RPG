#Get release
Invoke-WebRequest -Uri https://github.com/Cluck65/RPG/releases/download/rpg/PokemonYeah.zip -OutFile .\PokemonYeah.zip
Expand-Archive .\PokemonYeah.zip -DestinationPath .\ -Force

