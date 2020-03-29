<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editor.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>עריכת משחק</title>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="jScripts/jquery-1.12.0.min.js" type="text/javascript"></script>
    <script src="jScripts/JavaScript.js"></script>

</head>
<body>
    <form id="form2" runat="server">
        <ul class="nav nav-tabs justify-content-end">
            <li class="nav-item">
                <a class="nav-link active" href="#" data-toggle="modal" data-target="#myModal">אודות</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#">איך משחקים</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="game.aspx">משחק</a>
            </li>

        </ul>
        <div>

            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">

                    <!-- אודות מודל-->
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
            </div>


            <asp:Label ID="gameNameLabel" runat="server" Text=""></asp:Label>
            <br />
            <br />

        </div>
        <div id="container">

            <div id="quotesDiv" class="col-sm-6" style="background-color: salmon;" runat="server">
                ציטוט<br />
                <asp:TextBox ID="quoteTxt" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="quoteAlert" runat="server" CssClass="color:red"></asp:Label>
            </div>
            <div id="categoriesDiv" class="col-sm-6" style="background-color: lightsalmon;" runat="server">
                אתגר<br />
                <asp:TextBox ID="challengeTxt" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="challengeAlert" runat="server"></asp:Label>
            </div>

            &nbsp;&nbsp;

                    <asp:Button ID="addNewItemsBtn" CssClass="myButton" runat="server" Text="הוסף זוג פריטים" class="btn btn-dark" OnClick="addNewItemsBtn_click" />

            &nbsp;&nbsp;&nbsp;

                        <asp:GridView ID="challengeItemsGrid" DataSourceID="XmlDataSourcePath" OnRowCommand="Grid_RowCommand" runat="server" CellPadding="3" CellSpacing="2" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="idLabel0" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@id")%>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ציטוט">
                                    <ItemTemplate>
                                        <asp:Label ID="quoteLabel" runat="server" Text="" Visible="false"> </asp:Label>
                                        <asp:Image ID="itemImg0" runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="אתגר">
                                    <ItemTemplate>
                                        <asp:Label ID="challengeLabel" runat="server" Text="" Visible="false"> </asp:Label>
                                        <asp:Image ID="itemImg1" runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="עריכה">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="editImageButton0" runat="server" CommandName="editRow" ImageUrl="~/icons/edit.png" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="מחיקה">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="deleteImageButton0" runat="server" CommandName="deleteRow" ImageUrl="~/icons/delete.png" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#DFCAB5" Font-Bold="True" ForeColor="black" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="black" />
                        </asp:GridView>

            <br />

            <div class="row" runat="server">
            </div>

            <asp:XmlDataSource ID="XmlDataSourcePath" runat="server" DataFile="~/trees/games.xml" XPath=""></asp:XmlDataSource>
        </div>

    </form>
</body>
</html>

