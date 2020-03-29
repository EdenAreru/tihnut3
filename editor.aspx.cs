using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class _Default : System.Web.UI.Page
{
    //משתנים גלובליים
    string theGameId = "100"; //יכיל את קוד המשחק
    XmlNode selectedGameNode;//יכיל את המשחק הנבחר לעריכה
    XmlDocument myDoc = new XmlDocument();//פתיחת משתנה שיכיל את העץ XML
    protected void Page_Load(object sender, EventArgs e)//בטעינת העמוד
    {
        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/games.xml"));
        selectedGameNode = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]");
        //הדפסת שם המשחק
        gameNameLabel.Text = "דף עריכה למשחק התאמת ציטוטים בתחנת: " + selectedGameNode.Attributes["name"].Value;
        //מתן צבע לתוויות אזהרה
        quoteAlert.Attributes.Add("style", "color:red;");
        challengeAlert.Attributes.Add("style", "color:red;");

        //מסלול שאיבת התוכן לדאטהסורס
        XmlDataSourcePath.XPath = ("/routs/rout[@code=" + theGameId + "]/item");

        //לולאה הטוענת את הנתונים לריג וויאו
        foreach (GridViewRow gvr in challengeItemsGrid.Rows)//גריד וויאו של הפריטים
        {
            string myID = ((Label)gvr.FindControl("idLabel0")).Text; //קליטת האיידי 

            //אם זה פריט מסוג ציטוט
            if (myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id='" + myID + "']/@type").Value == "quote")
            {
                //הפריט מסוג ציטוט
                XmlNode myN = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id='" + myID + "']");
                //מציאת הפריט התואם לציטוט
                string matchNum = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id='" + myID + "']/@match").Value;
                XmlNodeList myMatches = myDoc.SelectNodes("/routs/rout[@code=" + theGameId + "]/item[@match='" + matchNum + "']");
                //הפריט התואם לציטוט
                XmlNode myMatchN = myMatches[1];


                if (myN.Attributes["isText"].Value == "false")//אם תמונה
                {
                    ((System.Web.UI.WebControls.Image)gvr.FindControl("itemImg0")).Visible = true;
                    ((System.Web.UI.WebControls.Image)gvr.FindControl("itemImg0")).ImageUrl = "/images/" + myN.InnerXml;
                    ((System.Web.UI.WebControls.Image)gvr.FindControl("itemImg0")).Height = 50;
                }
                else//אם טקסט
                {
                    //הזנת הנתונים בגריד וויאו
                    ((Label)gvr.FindControl("quoteLabel")).Visible = true;
                    ((Label)gvr.FindControl("quoteLabel")).Text = Server.UrlDecode(myN.InnerXml);
                    ((Label)gvr.FindControl("challengeLabel")).Visible = true;
                    ((Label)gvr.FindControl("challengeLabel")).Text = Server.UrlDecode(myMatchN.InnerXml);
                }
            }
            else //לא להראות שורות ריקות כאשר יש כפילויות
            {
                gvr.Visible = false;
            }

        }

    }

    protected void addNewItemsBtn_click(object sender, EventArgs e)//הוספת פריטים חדשים
    {
        //איפוס אזהרות
        quoteAlert.Text = "";
        challengeAlert.Text = "";

        if (quoteTxt.Text != "" && challengeTxt.Text != "")//אם יש תוכן בתיבות הטקסט
        {
            if (addNewItemsBtn.Text == "הוסף זוג פריטים")
            {
                //הפריט הראשון בעץ האקסמל
                XmlNode FirstAns = myDoc.SelectNodes("/routs/rout[@code=" + theGameId + "]/items").Item(0);

                // הקפצה של מונה האי.די. בתוך קובץ האקס.אם.אל באחד
                int myId = Convert.ToInt16(myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/@IDcounter").Value);
                string myNewId = (myId + 1).ToString();
                myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/@IDcounter").Value = myNewId.ToString();
                // הקפצה של מונה הההתאמות בתוך קובץ האקס.אם.אל באחד
                int myMatchNum = Convert.ToInt16(myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/@matchCounter").Value);
                string myNewMatchNum = (myMatchNum + 1).ToString();
                myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/@matchCounter").Value = myNewMatchNum.ToString();

                // יצירת ענף פריט     
                XmlElement myQuoteItem = myDoc.CreateElement("item");
                myQuoteItem.SetAttribute("id", myNewId);
                myQuoteItem.SetAttribute("match", myNewMatchNum);
                myQuoteItem.SetAttribute("type", "quote");//שונה
                myQuoteItem.SetAttribute("isText", "true");

                myQuoteItem.InnerXml = Server.UrlEncode(quoteTxt.Text);


                if (myId == 0)//הפריט הראשון שמוכנס
                {
                    myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]").AppendChild(myQuoteItem);
                    myDoc.Save(Server.MapPath("trees/games.xml"));
                    challengeItemsGrid.DataBind();

                }
                else//הוספת הפריט למעלה ברשימה
                {
                    // XmlNode FirstAns = myDoc.SelectNodes("/routs/rout[@code=" + theGameId + "]/items").Item(0);
                    myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]").InsertBefore(myQuoteItem, FirstAns);
                    myDoc.Save(Server.MapPath("trees/games.xml"));
                    challengeItemsGrid.DataBind();

                }

                myNewId = (Convert.ToInt32(myNewId) + 1).ToString();
                myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/@IDcounter").Value = myNewId.ToString();

                // יצירת ענף פריט    
                XmlElement myChallengeItem = myDoc.CreateElement("item");
                myChallengeItem.SetAttribute("id", myNewId);
                myChallengeItem.SetAttribute("match", myNewMatchNum);
                myChallengeItem.SetAttribute("type", "challenge");//שונה
                myChallengeItem.SetAttribute("isText", "true");

                myChallengeItem.InnerXml = Server.UrlEncode(challengeTxt.Text);

                //הוספת הפריט לעץ
                //XmlNode FirstAns = myDoc.SelectNodes("/routs/rout[@code=" + theGameId + "]/items").Item(0);
                myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]").InsertBefore(myChallengeItem, FirstAns);
                myDoc.Save(Server.MapPath("trees/games.xml"));
                challengeItemsGrid.DataBind();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                // ניקוי תיבת הטקסט
                quoteTxt.Text = "";
                challengeTxt.Text = "";
            }
            else //במידה ולחצו על עדכון פריטים
            {
                //מציאת הפריטים המתאימים שנלחצו עליהם ועדכון באקסמל
                XmlNode theQuoteItem = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + Session["editQuoteItemId"] + "]");
                XmlNode theChallengeItem = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + Session["editChallengeItemId"] + "]");

                theQuoteItem.InnerXml = Server.UrlEncode(quoteTxt.Text);
                theChallengeItem.InnerXml = Server.UrlEncode(challengeTxt.Text);
                myDoc.Save(Server.MapPath("trees/games.xml"));

                quoteTxt.Text = "";
                challengeTxt.Text = "";
                challengeItemsGrid.DataBind();
                addNewItemsBtn.Text = "הוסף זוג פריטים";
                Page.Response.Redirect(Page.Request.Url.ToString(), true);//רענון העמוד

            }
        }
        else //אם אין תוכן בתיבות הטקסט - הטענת אזהרות
        {
            if (quoteTxt.Text == "")
            {
                quoteAlert.Text = "יש להזין תוכן";
            }
            if (challengeTxt.Text == "")
            {
                challengeAlert.Text = "יש להזין תוכן";
            }


        }
    }

    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)//פונקציה שבודקת איזו שורה נלחצה ועל מה לחצו ומעבירה לפונקציות מתאימות
    {

        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה

        string theId = i.Attributes["theItemId"];

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                //deleteRow(theId);
                confirmationOpen(theId);//פתייחת פופא אפ לווידא מחיקה
                break;
            //אם נלחץ על כפתור עריכה (העפרון) נעבור לעריכת פריט                    
            case "editRow":
                editRow(theId);//פתיחת פונקציה של עריכת פריט

                //e.Row.BackColor = System.Drawing.Color.Cyan;
                break;
        }

    }

    void confirmationOpen(string theItemId)//פופ אם לוודוא מחיקת פריט
    {
        deleteItems(theItemId);//מעבר למחיקת פריט במידה והמשתמש אישר מחיקה
    }
    void editRow(string theItemId)//פופ אם לוודוא מחיקת פריט
    {
        //עריכת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו

        //מציאת הפריטים המתאימים
        //הפריט שנלחץ
        XmlNode theItem = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + theItemId + "]");
        //מציאת הפריט המתאים
        string itemMatchNum = myDoc.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + theItemId + "]/@match").Value;
        XmlNodeList myMatches = myDoc.SelectNodes("/routs/rout[@code=" + theGameId + "]/item[@match=" + itemMatchNum + "]");
        //הפריט המתאים
        XmlNode theMatchingItem = myMatches[1];

        //אם זה ציטוט אז תמלא את התוכן בתיבת בטקסט של פריט הציטוט
        if (theItem.Attributes["type"].Value == "quote")
        {
            quoteTxt.Text = Server.UrlDecode(theItem.InnerText);
            challengeTxt.Text = Server.UrlDecode(theMatchingItem.InnerText);
            Session["editQuoteItemId"] = theItemId;
            Session["editChallengeItemId"] = theMatchingItem.Attributes["id"].Value;
        }

        else        //אם זה אתגר אז תמלא את התוכן בתיבת בטקסט של פריט האתגר
        {
            challengeTxt.Text = Server.UrlDecode(theItem.InnerText);
            quoteTxt.Text = Server.UrlDecode(theMatchingItem.InnerText);
            Session["editQuoteItemId"] = theMatchingItem.Attributes["id"].Value;
            Session["editChallengeItemId"] = theItemId;

        }
        //שינוי המלל בכפתור
        addNewItemsBtn.Text = "עדכן זוג פריטים";

    }

    void deleteItems(string theItemId)
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDocument Document = XmlDataSourcePath.GetXmlDocument();

        //מציאת הפריט שנלחץ עליו
        XmlNode theItem = Document.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + theItemId + "]");
        //מציאת הפרט התואם
        string itemMatchNum = Document.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@id=" + theItemId + "]/@match").Value;
        //מחיקת הפריט הלחוץ
        theItem.ParentNode.RemoveChild(theItem);
        XmlNode theMatchingItem = Document.SelectSingleNode("/routs/rout[@code=" + theGameId + "]/item[@match=" + itemMatchNum + "]");
        //מחיקת הפריט התואם
        theMatchingItem.ParentNode.RemoveChild(theMatchingItem);
        //שמירה ורענון
        XmlDataSourcePath.Save();
        challengeItemsGrid.DataBind();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }


}
