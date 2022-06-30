using BackAuth.Data.Interface;
using BackAuth.Model.Response;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackAuth.Data.Service
{
    public class SendingEmailService : ISendingEmailService
    {
        private EmailConfiguration _emailConfiguration;
        public SendingEmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public int GenerateCode()
        {
            return new Random().Next(1000, 9999);
        }

        public string GenerateBodyHtml(int code, string name)
        {
            return $@"<table style=""width: 100 %; background - color:#ffffff;font-weight:300;font-family:Roboto,sans-serif,rebuchet MS;margin:0;font-size:16px;line-height:17px;color:#4a4a4a"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td style=""background:#fbfbfb""><table style=""width:100%;max-width:520px"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                            <tbody>
                                <tr>
                                    <td style=""text-align:center;background:#fbfbfb"">
                                        <table style=""max-width:520px;width:100%"" cellspacing=""0"" cellpadding=""0"">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table style= ""width:100%"" cellspacing=""0"" cellpadding=""0"">
                                                            <tbody>
                                                            <tr>
                                                                <td style=""width:100px;padding:20px 0px 20px 0px"" align=""center"">
                                                                    <img src="""" width=""140"" border=""0"" style=""margin-right:15px"">
                                                                </td>
                                                                <td style=""text-align:left;padding:20px 0px 20px 0px"" align=""left"">&nbsp;</td>
                                                            </tr>
                                                            </tbody>
                                                        </table>                                    
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table style=""margin-bottom:40px;max-width:570px;width:100%;background-color:#ffffff;border:1px solid #eaeeee;border-top:none;border-radius:5px"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                                            <tbody>
                                                <tr>
                                                    <td style=""text-align:center;border-bottom:1px solid #eaeeee;border-top:2px solid #1a73e8;padding:6px"">
                                                        <br>
                                                        <div style=""text-align:center;color:#9c9c9c;font-size:18px"">Notificación plataforma</div>
                                                        <div>
                                                            <table style=""padding:5%;text-align:left"" cellspacing=""0"" cellpadding=""0"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style=""padding-right:5%;min-width:60px"">
                                                                            <img src=""https://ci6.googleusercontent.com/proxy/a7pREI0NFDP1KOl0wGG7dtv2SIliKUtWW41I_2EVPOFKKYhS8XfCZRviFh6NsBMbv7XVtU2cJira6YRIrnSnKJJwJdFG0nrQiUDFZid4oA6y2NKQXJ5Hc7_RZQ=s0-d-e1-ft#http://aspirantes.ut.edu.co/images/inscripciones/correos/bienvenido1.png"" width=""50"" class=""CToWUd"">
                                                                        </td>
                                                                        <td style=""text-align:left"">
                                                                            <h1 style=""font-size:1.4em;line-height:1.2em;font-weight:400"">{name}, recibimos una solicitud para restablecer tu contraseña</h1>
                                                                        </td>
                                                                    </tr>    
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td style=""color:rgba(0,0,0,0.87);font-size:14px;padding-right:15%;text-align:left""> A continuación encontrarás toda la información:</td>
                                                                    </tr>                                                                   
                                                                </tbody>
                                                            </table>    
                                                        </div>                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style=""text-align:left;width:100%"" cellspacing=""0"" cellpadding=""0"">
                                                            <tbody>
                                                                <tr>
                                                                    <td style=""padding:20px 0px;width:50%"" colspan=""2"">
                                                                        <table cellspacing=""0"" cellpadding=""0"" align=""center"">   
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style=""padding-right:3%;min-width:40px"">
                                                                                        <img src=""https://ci5.googleusercontent.com/proxy/6H1Gt7PI_Y3Z0Zq8T6FNJp9npmj3pF14L2YrImxZvBrjiRFrNMKDyg4Gw53qLNi5HW4W8zRne8aaYXQOjs1nOYvTJwEPyrAHwh3EgBrhZ-FDBxFK0lN6=s0-d-e1-ft#http://aspirantes.ut.edu.co/images/inscripciones/correos/codigop.png"" width=""40"" height=""40"" style=""vertical-align:bottom"" class=""CToWUd"">
                                                                                    </td>
                                                                                    <td style=""min-width:120px"">
                                                                                        <span style=""font-size:18px;color:#9c9c9c;line-height:18px;font-weight:300;font-family:Roboto,sans-serif,rebuchet MS"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Token</span> 
                                                                                        <br>  
                                                                                        <span style=""font-size:2em;color:#1a73e8;line-height:35px;font-weight:300;font-family:Roboto,sans-serif,rebuchet MS""> &nbsp;&nbsp;&nbsp;&nbsp;{code} </span>  
                                                                                    </td> 
                                                                                </tr>  
                                                                            </tbody>
                                                                        </table> 
                                                                    </td>
                                                                </tr>                                                                                                                                
                                                            </tbody>
                                                                </table></td></tr><tr><td style= ""text-align:center;padding:20px;border-top:1px solid #eaeeee;color:rgba(0,0,0,0.54)""><span style=""text-align:center;padding:20px;font-size:10px;color:rgba(0,0,0,0.54)"">* En caso de no haber ingresado a plataforma, realice el cambio inmediatamente de su contraseña.</span> </td></tr></tbody></table></td></tr><tr valign =""top""><td style=""background:#fbfbfb"" valign=""top""><table style=""max-width:520px;width:97%;background:#fbfbfb"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                                                                </table></td></tr>
                                                                <tr>
                                                                    <td style=""padding-top:10px"" align=""center""><span style=""font-size:12px;color:rgba(0,0,0,0.75);font-weight:200;font-family:Roboto,sans-serif,Trebuchet MS""> Authentication - App | Todos los derechos reservados. | </span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style=""padding-top:10px"" align=""center""><span style=""font-size:11px;font-style:italic;color:rgba(0,0,0,0.75);font-weight:200;font-family:Roboto,sans-serif,Trebuchet MS""> Desarrollado por Eduardo Guarnizo.</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style=""padding-top:10px"" align=""center""> &nbsp;</td>
                                                                </tr>
                                                            </tbody>
                                                            </table>
                                                         </td>
                                                    </tr>      
                                               </tbody></table>";
        }

        public Task<bool> SendEmail(string to, string subject, string body)
        {
            MailMessage oMailMessage = new MailMessage(_emailConfiguration.Email, to, subject, body);            
            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient(_emailConfiguration.Host);
            oSmtpClient.Host = _emailConfiguration.Host;
            oSmtpClient.EnableSsl = _emailConfiguration.Ssl;
            oSmtpClient.UseDefaultCredentials = _emailConfiguration.DefaulCredentials;
            oSmtpClient.Port = _emailConfiguration.Port;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();

            return Task.FromResult(true);
        }
    }
}
