using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IronOcr;
using System.Web.Script.Serialization;
using System.Drawing;

namespace Gradware_OCR
{
	public partial class FormParser : System.Web.UI.Page
	{
		internal class UnspecifiedDoc
		{
			public string Text { get; set; }
			public double Confidence { get; set; }
		}

        internal class AcordDoc
        {
            public string Date { get; set; }
            public double DateConfidence { get; set; }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
		{
			string SelectedDocClass = DocumentClassDDL.SelectedValue;

            string FileName = FileUpload.PostedFile.FileName;
            string FileType = Path.GetExtension(FileName);

            // CHANGE: sets location file upload is saved in; currently set to user's desktop
            string FileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName);
            FileUpload.PostedFile.SaveAs(FileLocation);

			// if doc classification unspecified (accepts any doc)
			if (SelectedDocClass == "unspecified")
			{
				if (FileUpload.HasFile)
				{
					if (FileType == ".pdf")
					{
						// initializes list to be serialized into JSON
						var OutputList = new List<UnspecifiedDoc>();

                        // initializes IronOCR's AutoOcr object
                        var Ocr = new AutoOcr();

						var Result = Ocr.ReadPdf(FileLocation);

                        // parse content by paragraph
                        foreach (var page in Result.Pages)
						{
                            foreach (var paragraph in page.Paragraphs)
							{
								var paragraphText = paragraph.Text;
								double paragraphConfidence = Math.Round(paragraph.Confidence, 2);
                                
                                OutputList.Add(new UnspecifiedDoc() { Text = paragraphText, Confidence = paragraphConfidence });
                            }
						}

						// serialize list into JSON
						var serializer = new JavaScriptSerializer();
						var serializedOutputList = serializer.Serialize(OutputList);

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
					}
					else if (FileType == ".png" || FileType == ".jpg" || FileType == ".jpeg")
					{
                        // initializes list to be serialized into JSON
                        var OutputList = new List<UnspecifiedDoc>();

                        // initializes IronOCR's AutoOcr object; AutoOcr does not allow region cropping
                        var Ocr = new AutoOcr();

						var Result = Ocr.Read(FileLocation);

                        // parse content by paragraph
                        foreach (var page in Result.Pages)
                        {
                            foreach (var paragraph in page.Paragraphs)
                            {
                                var paragraphText = paragraph.Text;
                                double paragraphConfidence = Math.Round(paragraph.Confidence, 2);

                                OutputList.Add(new UnspecifiedDoc() { Text = paragraphText, Confidence = paragraphConfidence });
                            }
                        }

                        // serialize list into JSON
                        var serializer = new JavaScriptSerializer();
                        var serializedOutputList = serializer.Serialize(OutputList);

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
                    }
                    else
                    {
                        ResultTextArea.Text = "File type is not valid.";
                    }
				}
				else
				{
                    ResultTextArea.Text = "No file found.";
				}
			}

            // if doc classification is an ACORD 25 Form (typed)
			else if (SelectedDocClass == "acord25")
			{
                if (FileUpload.HasFile)
                {
                    if (FileType == ".pdf")
                    {
                        // initializes list to be serialized into JSON
                        var OutputList = new List<AcordDoc>();

                        // initializes IronOCR's AdvancedOcr object; AdvancedOcr allows for region cropping for PDFs
                        var Ocr = new AdvancedOcr()
                        {
                            EnhanceResolution = true,
                            EnhanceContrast = true,
                            CleanBackgroundNoise = true,
                            DetectWhiteTextOnDarkBackgrounds = true,
                            RotateAndStraighten = true,
                            ReadBarCodes = false,
                            ColorDepth = 4,
                            ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                            Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                            InputImageType = AdvancedOcr.InputTypes.Document,
                            Language = IronOcr.Languages.English.OcrLanguagePack
                        };

                        // retrieves date
                        var DateArea = new Rectangle(1915, 136, 360, 64); // (x, y, width, height)
                        var Date = Ocr.ReadPdf(FileLocation, DateArea);
                        string DateString = Date.ToString();

                        // retrieves contact name
                        var ContactNameArea = new Rectangle(1276, 450, 900, 50); // (x, y, width, height)
                        var ContactName = Ocr.ReadPdf(FileLocation, ContactNameArea);
                        string ContactNameString = ContactName.ToString();

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = ContactNameString;
                    }
                    else if (FileType == ".png" || FileType == ".jpg" || FileType == ".jpeg")
                    {
                        // TODO: Image ACORD 25 files
                    }
                }
                else
                {
                    ResultTextArea.Text = "No file found.";
                }
            }

            // if doc classification is a W9 Form (handwritten)
            else if (SelectedDocClass == "w9")
			{
                ResultTextArea.Text = "Processing for W9 Forms is unavailable at the moment.";

				if (FileUpload.HasFile)
				{
                    ResultTextArea.Text = "Processing for W9 Forms is unavailable at the moment.";
				}
				else
				{
                    ResultTextArea.Text = "No file found.";
				}
			}
		}
	}
}