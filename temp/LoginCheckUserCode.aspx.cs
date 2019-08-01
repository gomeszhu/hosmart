using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HOSmart.Framework.Foundation.Services;
using HOSmart.Framework.Foundation.Services.Support;
using System.Security.Cryptography;

public partial class LoginCheckUserCode : System.Web.UI.Page
{
    private ILogonService logonService = new LogonService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserCode = Request.Params["UserCode"];
			string Ret = "";
			string UserCheck = getMD5(UserCode);
            if (!string.IsNullOrEmpty(UserCode))
            {
                string Password = Request.Params["Password"];
                try
                {
					if (UserCheck == Password)
                    {
						Ret = "<Data><Ret>Y</Ret><Msg></Msg><UserCode>"+UserCode+"</UserCode></Data>";
                        Response.Write(Ret);
                        Response.End();
                        return;
                    }
                }
                catch (Exception ex)
                { 
                }
				Ret = "<Data><Ret>N</Ret><Msg>用户工号或密码错误！</Msg><UserCode></UserCode></Data>";
                Response.Write(Ret);
                Response.End();
            }
	    else
	    {
		Ret = "<Data><Ret>N</Ret><Msg>未输入用户工号信息！</Msg><UserCode></UserCode></Data>";
                Response.Write(Ret);
                Response.End();
	    }
        }
    }


    private string getMD5(string input)
    { 
        MD5 md = MD5CryptoServiceProvider.Create();
        byte[] hash;
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
        hash = md.ComputeHash(buffer);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}