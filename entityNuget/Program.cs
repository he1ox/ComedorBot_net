using System;
using System.Linq;
using System.Threading.Tasks;
using entityNuget.Sources;
using finalTelegram.Sources;
//Librerias TelegramBot
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace entityNuget
{
    class Program
    {
        /*EntityFramework 
         
         ¿Que es? 

            Es un framework ORM open-source desarrollado para .net,
            tiene como función mapear las estructuras de una base de datos relacional
            (en este caso SQL Server), sobre una clase de C#, con sus respectivos
            atributos extraídos directamente sobre las propiedades de la tabla.

            Así, la estructura de la BD, queda vinculada con las clases definidas por EF
            facilitando en gran manera el CRUD ( Create , Read, Update, Delete ).
         
         */
        static Models.DB.botDbContext db;

        //interface
        static ITelegramBotClient botClient; 
        static void Main(string[] args)
        {
            ConsoleSetup();
            //Instancia Base de datos
            db = new Models.DB.botDbContext();

            //Credentials contiene el token protegido
            //.gitignore para no exponerla en github
            botClient = new TelegramBotClient(Credentials.getToken());

            //Informacion del bot desde un metodo asincrono
            var me = botClient.GetMeAsync().Result;

            Console.Title = $"BOT =>{me.FirstName} CORRIENDO...";

            botClient.OnMessage += BotClient_OnMessage;
            botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            botClient.StartReceiving();


            Console.WriteLine("\nPresiona una tecla para detener la ejecución");
            Console.ReadKey();

            botClient.StopReceiving();
        }


        private static async void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var mensajeRecibido = e.Message.Text;
            var NombreUsuario = e.Message.Chat.FirstName;
            var ApellidoUsuario = e.Message.Chat.LastName;

            Models.DB.TbUsuario UsuarioDb = new Models.DB.TbUsuario();

            UsuarioDb.Nombre = NombreUsuario;
            UsuarioDb.Apellido = ApellidoUsuario;
            UsuarioDb.Mensaje = mensajeRecibido;

            db.TbUsuarios.Add(UsuarioDb);


            Console.WriteLine($"\nNUEVO MENSAJE [{message.Date.ToLocalTime()}], " +
                    $"\nDe: {NombreUsuario} {ApellidoUsuario}" +
                    $"\nContenido : {mensajeRecibido}");


            if (message == null || message.Type != MessageType.Text)
            {
                return;
            }

            switch (message.Text.Split(' ').First())
            {
                case "/ubicacion":
                    await MostrarUbicacion(message);
                    break;
                case "/ayuda":
                    await MostrarAyuda(message);
                    break;

                case "/contacto":
                    await MostrarContacto(message);
                    break;

                default:
                    await EnviarBienvenida(message);
                    break;
            }


            static async Task MostrarUbicacion(Message message)
            {
                var chatId = message.Chat;

                string Titulo_Ubicacion = $"Universidad UMG {EmojiList.School}";
                string Direccion_Ubicacion = $"RN-23, Jutiapa {EmojiList.Carretera}";
                //Direccion Fisica UMG
                var latitud = 14.28212207295388f;
                var longitud = -89.89679082145125f;


                string encabezadoTexto = $"Información de *Ubicacion* {EmojiList.Pushpin}\n\n" +
                                    "Jutiapa, Jutiapa\n" +
                                    "4ta.Calle “B” 4 - 30 zona 01\n" +
                                    "Callejón Medrano\n";

                string texto = "\n*Recuerda Siempre :*\n" +
                            $"\n1. Manejar con cuidado {EmojiList.Blue_Car}" +
                            $"\n2. Portar tu mascarilla en todo momento {EmojiList.Man}";



                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: encabezadoTexto + "" + texto,
                    replyToMessageId: message.MessageId,
                    parseMode: ParseMode.Markdown
                    );

                await botClient.SendVenueAsync(
                    chatId: chatId,
                    latitude: latitud,
                    longitude: longitud,
                    title: Titulo_Ubicacion,
                    address: Direccion_Ubicacion
                    );

            }


            static async Task MostrarContacto(Message message)
            {
                var chatId = message.Chat;

                /* El formato vCard se ha aplicado en el metodo sendContactAsync
                    
                ¿Que es?

                Es un formato estandarizado para el intercambio electrónico de información personal,
                es decir, es como enviar nuestra identificación personal (DPI) mediante el internet.

                 En el momento que el bot nos envía el contacto, podemos agregarlo y de manera inmediata
                se nos desplega toda la información del contacto agregada en cada uno de los campos en la
                aplicación de contactos de nuestro telefono.

                Desarrolado por: Internet Mail Consortium
                 */

                await botClient.SendChatActionAsync(chatId, ChatAction.Typing);

                //Simulando tiempo de respuesta 0.5s
                await Task.Delay(500);

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*Puedes consultar estos numeros* {EmojiList.Arrow_Down_Small}",
                    parseMode: ParseMode.Markdown
                    );

                await botClient.SendContactAsync(
                    chatId: chatId,
                    phoneNumber: "+50278833590",
                    firstName: $"PBX {EmojiList.Man}",
                    vCard: "BEGIN:VCARD\n" +
                            "VERSION:3.0\n" +
                            "N:PBX;UMG\n" +
                            "ORG:Consultas UMG Soporte\n" +
                            "TEL;TYPE=voice,work,pref:+50278833590\n" +
                            "EMAIL:info@umg.edu.gt\n" +
                            "END:VCARD",
                    replyToMessageId: message.MessageId
                    );

                await botClient.SendContactAsync(
                    chatId: chatId,
                    phoneNumber: "+50245791506",
                    firstName: $"Jorge {EmojiList.Robot_Face}",
                    vCard: "BEGIN:VCARD\n" +
                            "VERSION:3.0\n" +
                            $"N:Jorge;Lopez\n" +
                            "ORG:Desarrollador del Bot\n" +
                            "TEL;TYPE=voice,work,pref:+50245791506\n" +
                            "EMAIL:jlopezg112@miumg.edu.gt\n" +
                            "END:VCARD",
                    replyToMessageId: message.MessageId
                    );

            }

            static async Task MostrarAyuda(Message message)
            {

                string texto = "Este bot tiene como finalidad darle la oportunidad a los " +
                    "alumnos de la universidad *UMG*, la capacidad de ordenar y consultar los platos disponibles " +
                    $"desde una plataforma online {EmojiList.Coffee}\n\n" +
                    $"*Puedes visitar los enlaces listados abajo para mas información {EmojiList.Pushpin}:*";


                var botonesConUrl = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithUrl(
                            $"UMG - Sitio Oficial {EmojiList.Globe_With_Meridians}",
                            "https://www.umg.edu.gt/"
                            ),
                        InlineKeyboardButton.WithUrl(
                            $"Jorge - Github {EmojiList.Man}",
                            "https://github.com/he1ox"
                            )
                    },
                    new []
                    {
                        InlineKeyboardButton.WithUrl(
                            $"Reportar Bug {EmojiList.Bug}",
                            "mailto:jlopezg112@miumg.edu.gt"
                            ),
                        InlineKeyboardButton.WithUrl(
                            $"Documentación Bot {EmojiList.Robot_Face}",
                            "https://core.telegram.org/bots/api"
                            )
                    },
                    new []
                    {
                        InlineKeyboardButton.WithUrl(
                            $"Consejos Salud {EmojiList.Pill}",
                            "https://www.cdc.gov/healthyweight/spanish/healthyeating/index.html"
                            )
                    }
                });


                await botClient.SendPhotoAsync(
                    chatId: message.Chat,
                    caption: texto,
                    photo: "https://i.ibb.co/SKyw2bP/comedor.jpg",
                    parseMode: ParseMode.MarkdownV2,
                    replyToMessageId: message.MessageId,
                    replyMarkup: botonesConUrl
                    );
            }

            static async Task EnviarBienvenida(Message message)
            {
                var currentUserName = $"{message.Chat.FirstName} {message.Chat.LastName}";

                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    //Primer Fila
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Platos Disponibles", "Mostrar Platos"),
                        InlineKeyboardButton.WithCallbackData("Bebidas", "Bebidas"),
                    },
                    //Segunda Fila
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Ofertas", "Ofertas"),
                    }
                });


                Message mensajeFoto = await botClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    caption: $"{currentUserName}, Bienvenido al *COMEDOR UMG* {EmojiList.Wave}" +
                    $"\n\nPuedes elegir entre las siguientes opciones:",
                    parseMode: ParseMode.MarkdownV2,
                    photo: "https://ibb.co/2y1gBCp",
                    replyMarkup: inlineKeyboard
                    );
            }
        }

        private static async void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var messageFromCallback = e;

            //Obtiene info del chat
            var User = messageFromCallback.CallbackQuery.Message.Chat.FirstName;
            var UserDate = messageFromCallback.CallbackQuery.Message.Date.ToLocalTime();
            var MessageReceived = messageFromCallback.CallbackQuery.Data;


            Console.WriteLine($"\nInteracción detectada [{User} {UserDate} {MessageReceived}]");

            //Mensaje mostrado en la parte superior 
            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: e.CallbackQuery.Id,
                text: $"Consultando base de datos... {EmojiList.Robot_Face}"
                );

            await Task.Delay(200);

            /* Evaluamos el contenido devuelto por el inlinekeyboard */
            switch (messageFromCallback.CallbackQuery.Data)
            {
                case "Mostrar Platos":

                    await MostrarPlatos(messageFromCallback);
                    break;

                case "Ofertas":
                    await MostrarOfertas(messageFromCallback);
                    break;

                case "Bebidas":
                    await MostrarBebidas(messageFromCallback);
                    break;

                default:
                    Console.WriteLine("ENTRÓ");
                    break;

            }


            static async Task MostrarPlatos(CallbackQueryEventArgs mensaje)
            {
                var chatId = mensaje.CallbackQuery.Message.Chat;

                //EntityFramework - Obtiene lista de objetos TbProductos
                var lstProductos = db.TbProductos;

                //Metodo para simular que el bot escribe en la parte
                //superior de la pantalla
                await botClient.SendChatActionAsync(chatId, ChatAction.Typing);

                //Simulando tiempo de respuesta 0.5s
                await Task.Delay(500);

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*Platos Disponibles* {EmojiList.Arrow_Down_Small}",
                    parseMode: ParseMode.Markdown
                    );

                //Iterador para ir mostrando datos de la tabla tbProductos
                foreach (var product in lstProductos)
                {
                    //Simulando tiempo de respuesta 0.25s
                    await Task.Delay(250);

                    await botClient.SendPhotoAsync(
                        chatId: chatId,
                        caption: $"*{product.NombreProducto} {EmojiList.plato}*\n" +
                        $"Precio: *Q.{product.Precio} {EmojiList.Credit_Card}*\n\n" +
                        $"{EmojiList.Arrow_Right}_{product.Descripcion}_",
                        parseMode: ParseMode.Markdown,
                        photo: product.ImageUrl
                        );
                }
            }


            static async Task MostrarOfertas(CallbackQueryEventArgs mensaje)
            {
                var chatId = mensaje.CallbackQuery.Message.Chat;

                //EntityFramework - Obtiene lista de objetos  tbOfertas
                var lstOfertas = db.TbOfertas;

                await botClient.SendChatActionAsync(chatId, ChatAction.Typing);

                await Task.Delay(500);


                await botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: $"*Ofertas Disponibles* {EmojiList.Arrow_Down_Small}",
                   parseMode: ParseMode.Markdown
                   );

                foreach (var oferta in lstOfertas)
                {
                    await Task.Delay(250);

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"*{oferta.Titulo.ToUpper()} {EmojiList.Fire}*\n" +
                        $"Precio: *Q.{oferta.Precio} {EmojiList.Credit_Card}*\n\n" +
                        $"{EmojiList.Arrow_Right}_{oferta.Descripcion}_",
                        parseMode: ParseMode.Markdown
                        );
                }
            }

            static async Task MostrarBebidas(CallbackQueryEventArgs mensaje)
            {
                var chatId = mensaje.CallbackQuery.Message.Chat;

                //EntityFramework - Obtiene lista de objetos tbBebidas
                var lstBebidas = db.TbBebidas;

                await botClient.SendChatActionAsync(chatId, ChatAction.Typing);

                await Task.Delay(250);

                await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: $"*Bebidas Disponibles* {EmojiList.Arrow_Down_Small}",
                  parseMode: ParseMode.Markdown
                  );

                foreach (var bebida in lstBebidas)
                {
                    await Task.Delay(250);

                    await botClient.SendPhotoAsync(
                        chatId: chatId,
                        caption: $"*{bebida.NombreBebida} {EmojiList.Tropical_Drink}*\n" +
                        $"Precio: *Q.{bebida.Precio} {EmojiList.Credit_Card}*\n\n" +
                        $"{EmojiList.Arrow_Right}_{bebida.Descripcion}_",
                        parseMode: ParseMode.Markdown,
                        photo: bebida.ImageUrl
                        );
                }
            }
        }
    static private void ConsoleSetup()
    {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}
