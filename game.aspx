<%@ Page Language="C#" AutoEventWireup="true" CodeFile="game.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>משחק</title>
    <meta name="description" content="בעמוד זה ניתן לשחק את המשחק" />
    <meta name="keywords" content="משחק, מחולל" />
    <meta name="author" content="Eden Barayev & Korin Kashi" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <%--CSS--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <link href="Styles/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <link href="Styles/myStyle.css" rel="stylesheet" type="text/css" />
    <%--Scripts--%>

    <script src="jScripts/jquery-1.12.0.min.js" type="text/javascript"></script>
    <script src="jScripts/JavaScript.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">

        <ul class="nav nav-tabs justify-content-end">
            <li class="nav-item">
                <a class="nav-link active" href="#" data-toggle="modal" data-target="#myModal">אודות</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#">איך משחקים</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="editor.aspx">עורך</a>
            </li>

        </ul>
        <div id="container" aria-dropeffect="link">

            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">אודות</h4>
                        <p>המסע של harel</p>
                    </div>
                    <div class="modal-body">
                        <p>אפיון ופיתוח: קורין קאשי ועדן בראייב</p>
                        <br />
                        <p>
                            אופיין ופותח במסגרת פרויקט גמר תש"פ
                        </p>
                        <p>הפקולטה לטכנולוגיות למידה</p>
                        <p>המכון הטכנולוגי חולון</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>

            <p id="testing">התאימו את הציטוט לאתגר</p>
            עליכם להתאים 
            <span id="matchItemsNumSpan" runat="server"></span>
            זוגות

            <div class="row" runat="server">
                <div id="quotesDiv" class="col-sm-6" style="background-color: salmon;" runat="server">
                </div>
                <div id="categoriesDiv" class="col-sm-6" style="background-color: lightsalmon;" runat="server">
                </div>
            </div>

            <input id="check111" class="myButton" type="button" value="בדיקת תשובות" />
            <%--<asp:Label ID="message001" runat="server" Text="Label"></asp:Label>--%>
            <p id="message001" runat="server"></p>
            <br />
            <asp:Button ID="checkBtn" class="myButton" runat="server" Text="אינטראקציה הבאה" OnClick="checkBtn_Click" />
            <asp:Button ID="doToGameEditor" class="myButton" runat="server" Text="בחזרה למפה" OnClick="goToMap_Click" />
        </div>
    </form>
</body>
</html>
