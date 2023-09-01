﻿using System;
using System.Security.Policy;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace timesheetback.Services
{
	public class HelperService : IHelperService
	{
		public HelperService()
		{
		}

        private async Task SendEmail(string emailSubject, string emailTo, string nameTo, string plainTextContent, string htmlContent)
        {
            var apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("SendGrid")["ApiKey"];
            var fromEmail = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("SendGrid")["FromEmail"];
            var fromName = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("SendGrid")["FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subject = emailSubject;
            var to = new EmailAddress(emailTo, nameTo);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            System.Diagnostics.Debug.WriteLine(response.StatusCode);
        }

        public int GenerateRandom4DigitNumber()
        {
            Random random = new();
            return random.Next(1000, 10000);
        }

        public string GenerateRandomStringCode() {
            Guid newuuid = Guid.NewGuid();
            return newuuid.ToString();
        }

        public Task SendForgotPasswordEmail(string emailSubject, string emailTo, string nameTo, string code)
        {
            var forgotPassowrdUrl = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("SendGrid")["ForgotPasswordUrl"];

            var plainTextContent = $"If you forgot your password go to {forgotPassowrdUrl}?code={code} to reset it. \n <3 VegaIT ";

            var htmlContent = "";

            var htmlHead = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">\n<html data-editor-version=\"2\" class=\"sg-campaigns\" xmlns=\"http://www.w3.org/1999/xhtml\">\n    <head>\n      <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n      <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1\">\n      <!--[if !mso]><!-->\n      <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\">\n      <!--<![endif]-->\n      <!--[if (gte mso 9)|(IE)]>\n      <xml>\n        <o:OfficeDocumentSettings>\n          <o:AllowPNG/>\n          <o:PixelsPerInch>96</o:PixelsPerInch>\n        </o:OfficeDocumentSettings>\n      </xml>\n      <![endif]-->\n      <!--[if (gte mso 9)|(IE)]>\n  <style type=\"text/css\">\n    body {width: 600px;margin: 0 auto;}\n    table {border-collapse: collapse;}\n    table, td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}\n    img {-ms-interpolation-mode: bicubic;}\n  </style>\n<![endif]-->\n      <style type=\"text/css\">\n    body, p, div {\n      font-family: inherit;\n      font-size: 14px;\n    }\n    body {\n      color: #000000;\n    }\n    body a {\n      color: #000000;\n      text-decoration: none;\n    }\n    p { margin: 0; padding: 0; }\n    table.wrapper {\n      width:100% !important;\n      table-layout: fixed;\n      -webkit-font-smoothing: antialiased;\n      -webkit-text-size-adjust: 100%;\n      -moz-text-size-adjust: 100%;\n      -ms-text-size-adjust: 100%;\n    }\n    img.max-width {\n      max-width: 100% !important;\n    }\n    .column.of-2 {\n      width: 50%;\n    }\n    .column.of-3 {\n      width: 33.333%;\n    }\n    .column.of-4 {\n      width: 25%;\n    }\n    ul ul ul ul  {\n      list-style-type: disc !important;\n    }\n    ol ol {\n      list-style-type: lower-roman !important;\n    }\n    ol ol ol {\n      list-style-type: lower-latin !important;\n    }\n    ol ol ol ol {\n      list-style-type: decimal !important;\n    }\n    @media screen and (max-width:480px) {\n      .preheader .rightColumnContent,\n      .footer .rightColumnContent {\n        text-align: left !important;\n      }\n      .preheader .rightColumnContent div,\n      .preheader .rightColumnContent span,\n      .footer .rightColumnContent div,\n      .footer .rightColumnContent span {\n        text-align: left !important;\n      }\n      .preheader .rightColumnContent,\n      .preheader .leftColumnContent {\n        font-size: 80% !important;\n        padding: 5px 0;\n      }\n      table.wrapper-mobile {\n        width: 100% !important;\n        table-layout: fixed;\n      }\n      img.max-width {\n        height: auto !important;\n        max-width: 100% !important;\n      }\n      a.bulletproof-button {\n        display: block !important;\n        width: auto !important;\n        font-size: 80%;\n        padding-left: 0 !important;\n        padding-right: 0 !important;\n      }\n      .columns {\n        width: 100% !important;\n      }\n      .column {\n        display: block !important;\n        width: 100% !important;\n        padding-left: 0 !important;\n        padding-right: 0 !important;\n        margin-left: 0 !important;\n        margin-right: 0 !important;\n      }\n      .social-icon-column {\n        display: inline-block !important;\n      }\n    }\n  </style>\n      <!--user entered Head Start--><link href=\"https://fonts.googleapis.com/css?family=Lato&display=swap\" rel=\"stylesheet\"><style>\nbody {font-family: 'Lato', sans-serif;}\n</style><!--End Head user entered-->\n    </head>";
            var htmlLink = $"<a href=\"{forgotPassowrdUrl}?code={code}\" style=\"background-color:#fbde67; border:1px solid #fbde67; border-color:#fbde67; border-radius:0px; border-width:1px; color:#000000; display:inline-block; font-size:16px; font-weight:700; letter-spacing:0px; line-height:22px; padding:12px 18px 12px 18px; text-align:center; text-decoration:none; border-style:solid; font-family:inherit; width:180px;\" target=\"_blank\">ResetPassword</a>\n";
            var htmlBody = "\n    <body>\n      <center class=\"wrapper\" data-link-color=\"#000000\" data-body-style=\"font-size:14px; font-family:inherit; color:#000000; background-color:#FFFFFF;\">\n        <div class=\"webkit\">\n          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" class=\"wrapper\" bgcolor=\"#FFFFFF\">\n            <tr>\n              <td valign=\"top\" bgcolor=\"#FFFFFF\" width=\"100%\">\n                <table width=\"100%\" role=\"content-container\" class=\"outer\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\n                  <tr>\n                    <td width=\"100%\">\n                      <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\n                        <tr>\n                          <td>\n                            <!--[if mso]>\n    <center>\n    <table><tr><td width=\"600\">\n  <![endif]-->\n                                    <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; max-width:600px;\" align=\"center\">\n                                      <tr>\n                                        <td role=\"modules-container\" style=\"padding:0px 0px 0px 0px; color:#000000; text-align:left;\" bgcolor=\"#FFFFFF\" width=\"100%\" align=\"left\"><table class=\"module preheader preheader-hide\" role=\"module\" data-type=\"preheader\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;\">\n    <tr>\n      <td role=\"module-content\">\n        <p>Click the link to reset password</p>\n      </td>\n    </tr>\n  </table><table class=\"module\" role=\"module\" data-type=\"spacer\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"d651ef67-7f56-4ff0-bb05-fa04021373c1\">\n    <tbody>\n      <tr>\n        <td style=\"padding:0px 0px 30px 0px;\" role=\"module-content\" bgcolor=\"\">\n        </td>\n      </tr>\n    </tbody>\n  </table><table class=\"module\" role=\"module\" data-type=\"spacer\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"d651ef67-7f56-4ff0-bb05-fa04021373c1.1\">\n    <tbody>\n      <tr>\n        <td style=\"padding:0px 0px 30px 0px;\" role=\"module-content\" bgcolor=\"\">\n        </td>\n      </tr>\n    </tbody>\n  </table><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"7ca21845-dcb8-445a-80de-d1fdff002b08\" data-mc-module-version=\"2019-10-22\">\n    <tbody>\n      <tr>\n        <td style=\"padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;\" height=\"100%\" valign=\"top\" bgcolor=\"\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: center\"><span style=\"font-size: 24px; font-family: verdana, geneva, sans-serif\"><strong>TimeSheet VegaIT</strong></span></div><div></div></div></td>\n      </tr>\n    </tbody>\n  </table><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"7cd4c72c-c042-4c16-82f3-4aca5b1f0da3\" data-mc-module-version=\"2019-10-22\">\n    <tbody>\n      <tr>\n        <td style=\"padding:50px 40px 40px 40px; line-height:28px; text-align:inherit; background-color:#f8f8f8;\" height=\"100%\" valign=\"top\" bgcolor=\"#f8f8f8\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: inherit\"><span style=\"color: #000000; font-size: 24px; font-family: inherit\">Reset your password by clicking the link below...</span></div><div></div></div></td>\n      </tr>\n    </tbody>\n  </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"module\" data-role=\"module-button\" data-type=\"button\" role=\"module\" style=\"table-layout:fixed;\" width=\"100%\" data-muid=\"f4a77ac4-65c4-470a-a564-646acdd83093\">\n      <tbody>\n        <tr>\n          <td align=\"center\" bgcolor=\"#f8f8f8\" class=\"outer-td\" style=\"padding:0px 0px 30px 0px; background-color:#f8f8f8;\">\n            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"wrapper-mobile\" style=\"text-align:center;\">\n              <tbody>\n                <tr>\n                <td align=\"center\" bgcolor=\"#fbde67\" class=\"inner-td\" style=\"border-radius:6px; font-size:16px; text-align:center; background-color:inherit;\">\n                                  </td>\n                </tr>\n              </tbody>\n            </table>\n          </td>\n        </tr>\n      </tbody>\n    </table><table class=\"module\" role=\"module\" data-type=\"divider\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"b5ec95f4-9902-4d4d-acdc-8952190a117f\">\n    <tbody>\n      <tr>\n        <td style=\"padding:0px 0px 0px 0px;\" role=\"module-content\" height=\"100%\" valign=\"top\" bgcolor=\"\">\n          <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"100%\" height=\"10px\" style=\"line-height:10px; font-size:10px;\">\n            <tbody>\n              <tr>\n                <td style=\"padding:0px 0px 10px 0px;\" bgcolor=\"#fbde67\"></td>\n              </tr>\n            </tbody>\n          </table>\n        </td>\n      </tr>\n    </tbody>\n  </table><table class=\"wrapper\" role=\"module\" data-type=\"image\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"b5166920-fe70-4cb5-abe8-85faa2aca93d\">\n    <tbody>\n      <tr>\n        <td style=\"font-size:6px; line-height:10px; padding:0px 0px 0px 0px;\" valign=\"top\" align=\"center\">\n          <img class=\"max-width\" border=\"0\" style=\"display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;\" width=\"600\" alt=\"\" data-proportionally-constrained=\"true\" data-responsive=\"false\" src=\"http://cdn.mcauto-images-production.sendgrid.net/954c252fedab403f/82017d6b-d648-4b66-98b2-e55752bc2092/600x331.png\" height=\"331\">\n        </td>\n      </tr>\n    </tbody>\n  </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"100%\" role=\"module\" data-type=\"columns\" style=\"padding:0px 0px 0px 0px;\" bgcolor=\"#9ddaf7\" data-distribution=\"1,1\">\n    <tbody>\n      <tr role=\"module-content\">\n        <td height=\"100%\" valign=\"top\"><table width=\"300\" style=\"width:300px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\" border=\"0\" bgcolor=\"\" class=\"column column-0\">\n      <tbody>\n        <tr>\n          <td style=\"padding:0px;margin:0px;border-spacing:0;\"><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"4fa1d08b-76eb-4103-8827-b8192ee43e9d\" data-mc-module-version=\"2019-10-22\">\n    <tbody>\n      <tr>\n        <td style=\"padding:30px 30px 30px 30px; line-height:28px; text-align:inherit; background-color:#cdf997;\" height=\"100%\" valign=\"top\" bgcolor=\"#cdf997\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: inherit\"><span style=\"font-size: 14px; font-family: inherit\">July 06, 2019</span></div>\n<div style=\"font-family: inherit; text-align: inherit\"><br></div>\n<div style=\"font-family: inherit; text-align: inherit\"><span style=\"color: #000000; font-size: 24px; font-family: inherit\"><strong>Make your mark: </strong></span><span style=\"color: #000000; font-size: 24px; font-family: inherit\">Successful projects start with a defined vision. See how the Needle + Tech team kicked off an amazing campaign with </span><span style=\"color: #000000; font-size: 24px\">Be The Cause.</span></div><div></div></div></td>\n      </tr>\n    </tbody>\n  </table></td>\n        </tr>\n      </tbody>\n    </table><table width=\"300\" style=\"width:300px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\" border=\"0\" bgcolor=\"\" class=\"column column-1\">\n      <tbody>\n        <tr>\n          <td style=\"padding:0px;margin:0px;border-spacing:0;\"><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"4fa1d08b-76eb-4103-8827-b8192ee43e9d.1\" data-mc-module-version=\"2019-10-22\">\n    <tbody>\n      <tr>\n        <td style=\"padding:30px 30px 30px 30px; line-height:28px; text-align:inherit; background-color:#9ddaf7;\" height=\"100%\" valign=\"top\" bgcolor=\"#9ddaf7\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: inherit\"><span style=\"font-size: 14px; font-family: inherit\">July 08, 2019</span></div>\n<div style=\"font-family: inherit; text-align: inherit\"><br></div>\n<div style=\"font-family: inherit; text-align: inherit\"><span style=\"font-size: 24px; font-family: inherit\"><strong>Guest Speaker Series: </strong></span><span style=\"font-size: 24px; font-family: inherit\">Meet John Peace and listen to his journey as a developer for Orange.</span></div>\n<div style=\"font-family: inherit; text-align: inherit\"><br></div>\n<div style=\"font-family: inherit; text-align: inherit\"><a href=\"http://www.google.com\"><span style=\"font-size: 16px; font-family: inherit\"><u>RSVP Now!</u></span></a></div><div></div></div></td>\n      </tr>\n    </tbody>\n  </table></td>\n        </tr>\n      </tbody>\n    </table></td>\n      </tr>\n    </tbody>\n  </table><table class=\"wrapper\" role=\"module\" data-type=\"image\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"67ccb841-e1ce-4177-af36-5ea2096ed2c9\">\n    <tbody>\n      <tr>\n        <td style=\"font-size:6px; line-height:10px; padding:0px 0px 0px 0px;\" valign=\"top\" align=\"center\">\n          <img class=\"max-width\" border=\"0\" style=\"display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;\" width=\"600\" alt=\"\" data-proportionally-constrained=\"true\" data-responsive=\"false\" src=\"http://cdn.mcauto-images-production.sendgrid.net/954c252fedab403f/194206ab-f074-40a1-b992-86a48b1396b4/600x320.png\" height=\"320\">\n        </td>\n      </tr>\n    </tbody>\n  </table><table class=\"module\" role=\"module\" data-type=\"social\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"b0f2872b-0362-4d24-a2bf-a8f9b58f8839\">\n    <tbody>\n      <tr>\n        <td valign=\"top\" style=\"padding:30px 0px 0px 0px; font-size:6px; line-height:10px; background-color:#ffffff;\" align=\"center\">\n          <table align=\"center\" style=\"-webkit-margin-start:auto;-webkit-margin-end:auto;\">\n            <tbody><tr align=\"center\"><td style=\"padding: 0px 5px;\" class=\"social-icon-column\">\n      <a role=\"social-icon-link\" href=\"http://www.facebook.com\" target=\"_blank\" alt=\"Facebook\" title=\"Facebook\" style=\"display:inline-block; background-color:#000000; height:21px; width:21px;\">\n        <img role=\"social-icon\" alt=\"Facebook\" title=\"Facebook\" src=\"https://mc.sendgrid.com/assets/social/white/facebook.png\" style=\"height:21px; width:21px;\" height=\"21\" width=\"21\">\n      </a>\n    </td><td style=\"padding: 0px 5px;\" class=\"social-icon-column\">\n      <a role=\"social-icon-link\" href=\"http://www.facebook.com\" target=\"_blank\" alt=\"Twitter\" title=\"Twitter\" style=\"display:inline-block; background-color:#000000; height:21px; width:21px;\">\n        <img role=\"social-icon\" alt=\"Twitter\" title=\"Twitter\" src=\"https://mc.sendgrid.com/assets/social/white/twitter.png\" style=\"height:21px; width:21px;\" height=\"21\" width=\"21\">\n      </a>\n    </td><td style=\"padding: 0px 5px;\" class=\"social-icon-column\">\n      <a role=\"social-icon-link\" href=\"http://www.facebook.com\" target=\"_blank\" alt=\"Instagram\" title=\"Instagram\" style=\"display:inline-block; background-color:#000000; height:21px; width:21px;\">\n        <img role=\"social-icon\" alt=\"Instagram\" title=\"Instagram\" src=\"https://mc.sendgrid.com/assets/social/white/instagram.png\" style=\"height:21px; width:21px;\" height=\"21\" width=\"21\">\n      </a>\n    </td><td style=\"padding: 0px 5px;\" class=\"social-icon-column\">\n      <a role=\"social-icon-link\" href=\"www.facebook.com\" target=\"_blank\" alt=\"LinkedIn\" title=\"LinkedIn\" style=\"display:inline-block; background-color:#000000; height:21px; width:21px;\">\n        <img role=\"social-icon\" alt=\"LinkedIn\" title=\"LinkedIn\" src=\"https://mc.sendgrid.com/assets/social/white/linkedin.png\" style=\"height:21px; width:21px;\" height=\"21\" width=\"21\">\n      </a>\n    </td></tr></tbody>\n          </table>\n        </td>\n      </tr>\n    </tbody>\n  </table><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"e8126f56-1e7e-4244-b63f-9e01c85aa48d\" data-mc-module-version=\"2019-10-22\">\n    <tbody>\n      <tr>\n        <td style=\"padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;\" height=\"100%\" valign=\"top\" bgcolor=\"\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: center\"><span style=\"font-size: 16px; font-family: arial, helvetica, sans-serif\">VegaIT</span></div><div></div></div></td>\n      </tr>\n    </tbody>\n  </table><div data-role=\"module-unsubscribe\" class=\"module\" role=\"module\" data-type=\"unsubscribe\" style=\"color:#444444; font-size:12px; line-height:20px; padding:16px 16px 16px 16px; text-align:Center;\" data-muid=\"4e838cf3-9892-4a6d-94d6-170e474d21e5\"><div class=\"Unsubscribe--addressLine\"></div><p style=\"font-size:12px; line-height:20px;\"><a target=\"_blank\" class=\"Unsubscribe--unsubscribeLink zzzzzzz\" href=\"{{unsubscribe}}}\" style=\"\">Unsubscribe</a> - <a href=\"{{unsubscribe_preferences}}}\" target=\"_blank\" class=\"Unsubscribe--unsubscribePreferences\" style=\"\">Unsubscribe Preferences</a></p></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"module\" data-role=\"module-button\" data-type=\"button\" role=\"module\" style=\"table-layout:fixed;\" width=\"100%\" data-muid=\"0644ff51-38f7-4da6-bb03-37fee41f8046\">\n      <tbody>\n        <tr>\n          <td align=\"center\" bgcolor=\"\" class=\"outer-td\" style=\"padding:0px 0px 20px 0px;\">\n            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"wrapper-mobile\" style=\"text-align:center;\">\n              <tbody>\n                <tr>\n                <td align=\"center\" bgcolor=\"#f5f8fd\" class=\"inner-td\" style=\"border-radius:6px; font-size:16px; text-align:center; background-color:inherit;\"><a href=\"https://www.sendgrid.com/?utm_source=powered-by&utm_medium=email\" style=\"background-color:#f5f8fd; border:1px solid #f5f8fd; border-color:#f5f8fd; border-radius:25px; border-width:1px; color:#a8b9d5; display:inline-block; font-size:10px; font-weight:normal; letter-spacing:0px; line-height:normal; padding:5px 18px 5px 18px; text-align:center; text-decoration:none; border-style:solid; font-family:helvetica,sans-serif;\" target=\"_blank\">♥ POWERED BY TWILIO SENDGRID</a></td>\n                </tr>\n              </tbody>\n            </table>\n          </td>\n        </tr>\n      </tbody>\n    </table></td>\n                                      </tr>\n                                    </table>\n                                    <!--[if mso]>\n                                  </td>\n                                </tr>\n                              </table>\n                            </center>\n                            <![endif]-->\n                          </td>\n                        </tr>\n                      </table>\n                    </td>\n                  </tr>\n                </table>\n              </td>\n            </tr>\n          </table>\n        </div>\n      </center>\n    </body>\n  </html>";

            htmlContent += htmlHead;
            htmlContent += htmlLink;
            htmlContent += htmlBody;

            return SendEmail(emailSubject, emailTo, nameTo, plainTextContent, htmlContent);
        }
    }
}

