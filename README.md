# DBEntityMocker
[![pipeline status](https://gitlab.com/AzRunRCE/eni-java-project-bid/badges/dev/pipeline.svg)](https://gitlab.com/AzRunRCE/eni-java-project-bid/commits/dev)
[![coverage report](https://gitlab.com/AzRunRCE/eni-java-project-bid/badges/dev/coverage.svg)](https://gitlab.com/AzRunRCE/eni-java-project-bid/commits/dev)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/your/your-project/blob/master/LICENSE)

DBEntityMocker, because sometime it's boring to prepare large data for unit tests, i have develop a litle software that extract data from a existing database with a specifiq SQL request and generate code to hydrate mock object in C#.

# Getting Started
- Open app.config and set connection string to the database source.
```xml <connectionStrings>
      <add name="DeffaultConnection" connectionString="..." />
</connectionStrings>
```
- Open a .NET dll where a DBContext derived class is implemented. Often it's where is located the .edmx file.

- Select an entity with the combobox.

- Edit the sql request to get data you want use to hidrate.

- Press Okay button.

- Enjoy from automated hydrating code.



## Built With
Visual Studio 2017 - .NET Framework 4.6

## Contributing
Please read [CONTRIBUTING.md](./CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning
We use GIT for versioning. For the versions available, see the tags on this repository.

## Team

| <a href="http://github.com/AzRunRCE" target="_blank">**Quentin Martinez @AzRunRCE**</a>  |
| :---: 
| [![ Quentin Martinez @AzRunRCE](https://avatars0.githubusercontent.com/u/20741531?s=200)](http://azrunsoft.com)    | [![Fabien Catin @fbnctn ](https://secure.gravatar.com/avatar/75be2983e928aaf4f3d30e9ddff02cae?s=180&d=identicon)](https://gitlab.com/fbnctn) | [![Romain @ApoZLd](https://assets.gitlab-static.net/uploads/-/system/user/avatar/3144065/avatar.png?width=90)](https://gitlab.com/ApoZLd)  |
| <a href="http://github.com/AzRunRCE" target="_blank">`github.com/AzRunRCE`</a> | <a href="https://gitlab.com/fbnctn" target="_blank">`gitlab.com/fbnctn`</a> | <a href="https://gitlab.com/ApoZLd" target="_blank">`gitlab.com/ApoZLd`</a> |



## License
This project is licensed under the MIT License - see the [license.md](./license.md) file for details

## Donations 
- AzRun BTC 1HP2GCUkZv4f5hVEamnUHfBNfcy2HGWYot
