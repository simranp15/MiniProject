<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Assignment_1.Validator" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function checks() {
            var name = document.getElementById("TextBox1").value;
            var familyName = document.getElementById("TextBox2").value;
            var address = document.getElementById("TextBox3").value;
            var city = document.getElementById("TextBox4").value;
            var zipCode = document.getElementById("TextBox5").value;
            var phone = document.getElementById("TextBox6").value;
            var email = document.getElementById("TextBox7").value;
            var errors = [];

            
            if (name.trim() === '') {
                errors.push("Name is required.");
                document.getElementById("RequiredFieldValidator1").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator1").style.display = "none";
            }
            if (familyName.trim() === '') {
                errors.push("Family name is required.");
                document.getElementById("RequiredFieldValidator2").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator2").style.display = "none";
            }
            if (familyName.trim() === name.trim()) {
                errors.push("Family name should differ from Name.");
            }
            if (address.trim().length < 2) {
                errors.push("Address should be at least 2 characters.");
                document.getElementById("RequiredFieldValidator3").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator3").style.display = "none";
            }
            if (city.trim().length < 2) {
                errors.push("City should be at least 2 characters.");
                document.getElementById("RequiredFieldValidator4").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator4").style.display = "none";
            }
            if (!/^\d{5}$/.test(zipCode)) {
                errors.push("Zip code should be 5 digits.");
                document.getElementById("RequiredFieldValidator5").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator5").style.display = "none";
            }
            if (!/^\d{2}-\d{7}$/.test(phone) && !/^\d{3}-\d{7}$/.test(phone)) {
                errors.push("Phone number should be in the format XX-XXXXXXX or XXX-XXXXXXX.");
                document.getElementById("RequiredFieldValidator6").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator6").style.display = "none";
            }
            if (!/\S+@\S+\.\S+/.test(email)) {
                errors.push("Email is invalid.");
                document.getElementById("RequiredFieldValidator7").style.display = "inline";
            } else {
                document.getElementById("RequiredFieldValidator7").style.display = "none";
            }

            var errorList = document.getElementById("errorList");
            errorList.innerHTML = "";
            if (errors.length > 0) {
                errors.forEach(function (error) {
                    var li = document.createElement("li");
                    li.textContent = error;
                    errorList.appendChild(li);
                });
                alert("Validation Errors:\n" + errors.join("\n"));
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <h3>Insert Your Details:</h3>
        
        Name: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is Required" ControlToValidate="TextBox1" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>

        <br />
        <br />
        
        Family Name: &nbsp; &nbsp;&nbsp; <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Family Name" ControlToValidate="TextBox2" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox2" ControlToCompare="TextBox1" Operator="NotEqual" ErrorMessage="Family name should differ from Name." ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:CompareValidator>

        <br />
        <br />
        
        Address: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Address" ControlToValidate="TextBox3" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Address should have at least 2 characters." ControlToValidate="TextBox3" ValidationExpression="^.{2,}$" ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:RegularExpressionValidator>
        
        <br />
        <br />
        
        City: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter City" ControlToValidate="TextBox4" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="City should have at least 2 characters." ControlToValidate="TextBox4" ValidationExpression="^.{2,}$" ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:RegularExpressionValidator>
        
        <br />
        <br />
        
        Zip Code: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Zip Code" ControlToValidate="TextBox5" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Zip-code should be 5 digits." ControlToValidate="TextBox5" ValidationExpression="^\d{5}$" ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:RegularExpressionValidator>
        
        <br />
        <br />
        
        Phone: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Phone No" ControlToValidate="TextBox6" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Phone should be in the format XX-XXXXXXXX or XXX-XXXXXXX." ControlToValidate="TextBox6" ValidationExpression="^\d{2,3}-?\d{7}$" ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:RegularExpressionValidator>
        
        <br />
        <br />
        
        E-Mail: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Email ID" ControlToValidate="TextBox7" ForeColor="Red" Display="Dynamic" ValidationGroup="newValidationGroup">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Email Format." ControlToValidate="TextBox7" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ForeColor="Red"  ValidationGroup="newValidationGroup"></asp:RegularExpressionValidator>
        
        <br />
        <br />
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="newValidationGroup"/>
        
        
        <asp:Button ID="btn" runat="server" Text="Check" Style="float: right;" OnClientClick="return checks();" ValidationGroup="newValidationGroup" OnClick="btnSubmit_Click" Height="33px" Width="136px" />
        
        <ul id="errorList" style="color: red;"></ul>
    </form>
</body>
</html>
