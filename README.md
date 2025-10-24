# Gestor De Pule

This is a complete management system for betting control. The program allows for the full registration of Animals (competitors) and Bettors (clients). Its main function is to securely register each Pule (bet), tracking amounts and payments. The key feature is the Reports module, which allows filtering by any bettor to generate a detailed statement of all their plays, values, and total bets.


[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/) [![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/) [![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)
 <img src="https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white"/> <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white"/>

## Stack

**Front-end:** Form, WPF

**Back-end:** C#, .NET


## installation

Install my-project with  dotnet

```bash
  dotnet publish -c Release -r win-x64 --self-contained true
```
## Project Window
![Main Window](https://github.com/lopes-obregon/Gestor-De-Pule/blob/master/mainWindow?raw=true)
![Window Data](https://github.com/lopes-obregon/Gestor-De-Pule/blob/master/FormData?raw=true)
![Input Data](https://github.com/lopes-obregon/Gestor-De-Pule/blob/master/inputData?raw=true)
## Project Features
- `Registry Management`: Allows for the creation, modification, deletion, and searching of Animals (competitors, owners, etc.) and Bettors (clients).
- `Bet Registration (Pules)`: Records each new bet into the system, linking the bettor, the selected animals (single, double, etc.), and the bet amount.
- `Payment Control`: (related to Registration) Allows for managing the status of each pule (e.g., "Pending" or "Paid"), ensuring a receipt is only issued after confirmation.
- `Report Generation`: Generates a detailed statement per bettor, allowing filtering by date and listing all their plays, values, and a summary of total pules.


## Actors

[<img loading="lazy" src="https://avatars.githubusercontent.com/u/45721862?v=4" width=115><br><sub>Renan Lopes Obregon</sub>](https://github.com/lopes-obregon)

## Contributing
Contributions are always welcome!
Please follow the `code of conduct` of this project.

