<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormParser.aspx.cs" Inherits="Gradware_OCR.FormParser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100vh;">
<head runat="server">
    <title>Form Parser</title>
	<style type="text/css">
        body {
            margin: 80px auto;
            max-width: 60vw;
            font-family: sans-serif;
            text-align: center;
            background: rgb(255,255,255);
            background: linear-gradient(135deg, rgba(255,255,255,1) 0%, rgba(101,162,245,1) 98%, rgba(52,138,255,1) 100%);
        }
	</style>
</head>
<body>
    <form id="DocUploadForm" runat="server">
        <div>
            <asp:Label ID="PromptLabel" runat="server" Text="Please select a form classification and upload your file."></asp:Label>
			
            <br />
        	<br />

        	<asp:DropDownList ID="DocumentClassDDL" runat="server" TabIndex="0" style="margin-left: 65px;">
				<asp:ListItem Text="Unspecified" Value="unspecified"></asp:ListItem>
				<asp:ListItem Text="ACORD 25" Value="acord25"></asp:ListItem>
				<asp:ListItem Text="W9" Value="w9"></asp:ListItem>
			</asp:DropDownList>

        	<asp:FileUpload ID="FileUpload" runat="server" />

			<br />
			<br />

            <div style="font-size: 12px;">Note: Parsing image files may take longer than PDFs.</div>
			
            <br />

			<asp:Button ID="UploadButton" Text="Extract Values" runat="server" OnClick="UploadButton_Click" />

			<br />
			<br />

			<asp:TextBox id="ResultTextArea" TextMode="multiline" runat="server" ReadOnly="True" style="width: 25vw; height: 60vh" />

			<br />
			<br />

            <button onclick="parseJson()">Parse JSON</button>

            <script>
                function parseJson() {
                    event.preventDefault(); // prevents ASP.NET from refreshing
                    let textAreaVal = document.getElementById('ResultTextArea').value;
                    const obj = JSON.parse(textAreaVal)[0];
                    const result = Object.keys(obj).map((key, i) => `${key}:\t${Object.values(obj)[i]}\n`).join('');
                    document.getElementById('ResultTextArea').value = result;
                }
            </script>
        </div>
    </form>
</body>
</html>
