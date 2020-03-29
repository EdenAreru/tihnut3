using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


public partial class Default2 : System.Web.UI.Page
{
    //משתנים גלובליים
    string theGameCode; //יכיל את קוד המשחק
    XmlNode selectedGameNode;//יכיל את המשחק הנבחר לעריכה
    XmlDocument myDoc = new XmlDocument();//פתיחת משתנה שיכיל את העץ XML

    protected void Page_Load(object sender, EventArgs e)//בזמן הטענת העמוד
    {
        //הטענת נתונים
        myDoc.Load(Server.MapPath("trees/games.xml"));
        theGameCode = "100";//ישתנה לסיישן כשיהיו עוד משחקים


        var totalItems = Convert.ToInt16(myDoc.SelectSingleNode("/routs/rout[@code=" + theGameCode + "]/@IDcounter").Value);
        matchItemsNumSpan.InnerText = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameCode + "]/@matchCounter").Value;

        var dragIdNum = 0; //משתנים לסידור המסרים של הפריטים
        var dropIdNum = 0;

        //לולאה הרצה על כל הפריטים בעץ ושמה את הנתונים בעמוד
        for (var i = 1; i <= totalItems; i++)
        {
            //אם זה פריטים מסוג ציטוט
            if (myDoc.SelectSingleNode("/routs/rout[@code=" + theGameCode + "]/item[" + i + "]/@type").Value == "quote")

            {   //הגדלת המונה        
                dragIdNum++;
                //הוספת הפריטים לעמודת הציטוטים 
                quotesDiv.Controls.Add(new Literal()
                {
                    Text = "<p id='drag" + dragIdNum + "' runat='server' class='dragItems' ondragstart='drag(event)' draggable='true'>" +
               Server.UrlDecode(myDoc.SelectSingleNode("/routs/rout[@code=" + theGameCode + "]/item[" + i + "]").InnerXml) + "</p>"
                });

            }
            else // אם זה פריטים מסוג אתגר
            {
                dropIdNum++;//הגדלת המונה
                //הוספת הפריטים לעמודת האתגרים
                categoriesDiv.Controls.Add(new Literal()
                {
                    Text = "<div id='drop" + dropIdNum + "' class='dragOver' ondrop='drop(event)' ondragover='allowDrop(event)'> " +
             Server.UrlDecode(myDoc.SelectSingleNode("/routs/rout[@code=" + theGameCode + "]/item[" + i + "]").InnerXml) + "</div>"
                });

            }
        }

    }


    protected void checkBtn_Click(object sender, EventArgs e)//פונקציה למעבר לפעילות הבאה - סליידר של תמונות
    {
        Response.Redirect("slider2020.html");//מעבר 

    }
    protected void goToMap_Click(object sender, EventArgs e)//בלחיצה על כפתור חזרה לעמוד המפה
    {
        Response.Redirect("mapVideo.html");
    }
}