<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormParser.aspx.cs" Inherits="Gradware_OCR.FormParser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100vh;">
<head runat="server">
    <title>Form Parser</title>
	<style type="text/css">
        body {
            margin: 0px auto;
            max-width: 60vw;
            font-family: sans-serif;
            text-align: center;
            background: rgb(255,255,255);
            background: linear-gradient(135deg, rgb(217, 255, 246) 0%, rgb(47, 135, 255) 100%);
        }
	</style>
</head>
<body>
    <form id="DocUploadForm" runat="server">
        <div style="margin-top: 80px;">
            <asp:Label ID="PromptLabel" runat="server" Text="Please select a form classification and upload your file."></asp:Label>
			
            <br />
        	<br />

        	<asp:DropDownList ID="DocumentClassDDL" runat="server" TabIndex="0" style="margin-left: 65px;">
				<asp:ListItem Text="Unspecified" Value="unspecified"></asp:ListItem>
				<asp:ListItem Text="ACORD 25" Value="acord25"></asp:ListItem>
				<asp:ListItem Text="W-9" Value="w9"></asp:ListItem>
			</asp:DropDownList>

        	<asp:FileUpload ID="FileUpload" runat="server" />

			<br />
			<br />

            <div style="font-size: 12px;">Note: Parsing image files may take longer than PDFs.</div>
			
            <br />

			<asp:Button ID="UploadButton" Text="Extract Values" runat="server" OnClick="UploadButton_Click" />

			<br />
			<br />

            <div id="ResultDiv" runat="server" style="
                width: 32vw;
                height: 62vh;
                margin: auto;
                padding: 20px;
                overflow: auto;
                word-break: break-word;
                background-color: rgb(255, 255, 255, 0.5);
            "></div>

			<br />
			<br />

            <button onclick="parseJson()">Parse JSON</button>

            <script>
                const textAreaVal = document.getElementById('ResultDiv').innerHTML;

                function parseJson() {
                    event.preventDefault(); // prevents ASP.NET from refreshing
                    const obj = JSON.parse(textAreaVal)[0];
                    const result = Object.keys(obj).map((key, i) => {
                        let confidenceColor = null;
                        if (/confidence/ig.test(key)) {
                            if (Object.values(obj)[i] >= 90) { confidenceColor = "background-color: #b2ffb2;" }
                            else if (Object.values(obj)[i] < 90 && Object.values(obj)[i] >= 80) { confidenceColor = "background-color: #ffeab2;" }
                            else { confidenceColor = "background-color: #ffb2b2;" }
                        }
                        return `<p style="${"margin: 0px; padding: 5px; ".concat(confidenceColor)}">${key}:\t${Object.values(obj)[i]}</p>`
                    }).join('');
                    document.getElementById("ResultDiv").innerHTML = result;
                }
            </script>
        </div>
    </form>
</body>
</html>
