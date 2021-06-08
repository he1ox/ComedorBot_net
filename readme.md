# TelegramBot en .NET  🚀
 
<img align="center" src="https://cdn.andro4all.com/files/2021/04/Mejores-bots-Telegram.jpg" alt="portada" >

<p align="center">
    <img alt="dotnet-version" src="https://img.shields.io/badge/.NET CORE-%3E%3D4.0-blue.svg"></img>
    <img alt="csharp-version" src="https://img.shields.io/badge/C%23-9.0-blue.svg"></img>
    <img alt="IDE-version" src="https://img.shields.io/badge/IDE-VS 2019-blue.svg"></img

</p>

<p align="center">
     <img alt="GIT" src="https://img.shields.io/badge/-Git-black?style=flat-square&logo=git"></img>
     <img alt="GIT" src="https://img.shields.io/badge/-GitHub-181717?style=flat-square&logo=github"></img>
     
 </p>

## Construido con 🛠️ (Aplica como requisitos)
* [Visual Studio](https://visualstudio.microsoft.com/es/) - Microsoft Visual Studio es un entorno de desarrollo integrado para Windows y macOS. Es compatible con múltiples lenguajes de programación, tales como C++, C#, Visual Basic


* [C# 9.0](https://docs.microsoft.com/en-us/dotnet/csharp/) - "C#" es un lenguaje de programación multiparadigma desarrollado y estandarizado por la empresa Microsoft como parte de su plataforma .NET


* [.NET CORE 5.0](https://es.wikipedia.org/wiki/.NET_Core) - .NET es un framework informático administrado, gratuito y de código abierto para los sistemas operativos Windows, Linux y macOS. ​ Es un sucesor multiplataforma de .NET Framework.​​ El proyecto es desarrollado principalmente por Microsoft bajo la Licencia MIT.​ 

* [SQL SERVER](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) - SQL Server es un sistema de gestión de bases de datos relacional desarrollado bajo licencia dual: Licencia pública general/Licencia comercial por Oracle Corporation y está considerada como la base de datos de código abierto más popular del mundo

* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - Framework ORM para mapear base de datos relacionales a clases.

* [Telegram Bot Api](https://core.telegram.org/bots/api) - Bot API es una interfaz basada en HTTP creada para desarrolladores interesados ​​en crear bots para Telegram.
Para aprender a crear y configurar un bot.

## Autores ✒️

* **Jorge Lopez** - [GitHub](https://github.com/he1ox) 


## Configura tu Token 🔧
```CSHARP
class Credentials
    {
        private static string Token = "Ingresa_tu_Token";

        public static string getToken()
        {
            return Token;
        }
    }

    /*Credentials contiene el token protegido
    si vas a subir tu repositorio a github, añade 
    en la clase Credentials.cs tu token y llama al metodo,
    asegurate de tener en tu .gitignore dicha clase para que 
    no la meta en el staging */
    
    botClient = new TelegramBotClient(Credentials.getToken());
    
```

## Presentación 🚀
<img align="center" src="https://github.com/he1ox/ComedorBot_net/blob/main/Imagenes/presentacion.jpg" alt="img" />
<img align="center" src="https://github.com/he1ox/ComedorBot_net/blob/main/Imagenes/presentacion2.jpg" alt="img1" />
<img align="center" src="https://github.com/he1ox/ComedorBot_net/blob/main/Imagenes/presentacion3.jpg" alt="img1" />


### ⌨️  Bendiciones!

![Visitor Badge](https://visitor-badge.laobi.icu/badge?page_id=he1ox.ComerdorBot_net)
