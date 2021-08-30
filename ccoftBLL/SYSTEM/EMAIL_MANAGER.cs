/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ccoftBLL.SYSTEM
{
    public class EMAIL_MANAGER
    {
        public bool f_gSendEmail(string p_sHtml, List<string> p_sEmailList, List<string> p_sBccEmailList, string p_sSubject)
        {
            bool l_bControl = true;
            try
            {
                SmtpClient l_cSmtpClient = new SmtpClient();
                l_cSmtpClient.Port = 587;
                l_cSmtpClient.Host = APPLICATION.EMAIL_SERVER;
                l_cSmtpClient.EnableSsl = false;
                l_cSmtpClient.Credentials = new NetworkCredential(APPLICATION.EMAIL, APPLICATION.PASSWORD);
                MailMessage l_cEmail = new MailMessage();
                l_cEmail.From = new MailAddress(APPLICATION.EMAIL, "✉ " + APPLICATION.APPLICATION_NAME);
                for (int i = 0; i < p_sEmailList.Count; i++)
                {
                    l_cEmail.To.Add(p_sEmailList[i]);
                }
                if (p_sBccEmailList != null)
                {
                    for (int i = 0; i < p_sBccEmailList.Count; i++)
                    {
                        l_cEmail.Bcc.Add(p_sBccEmailList[i]);
                    }
                }
                l_cEmail.Subject = "✉ " + p_sSubject;
                l_cEmail.IsBodyHtml = true;
                l_cEmail.Body = p_sHtml;
                object l_cUserState = l_cEmail;
                l_cSmtpClient.Send(l_cEmail);
            }
            catch (Exception e)
            {
                l_bControl = false;
            }
            return l_bControl;
        }
    }
}
