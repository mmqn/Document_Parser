using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using IronOcr;
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
            public string Producer { get; set; }
            public string Insured { get; set; }
            public string ContactName { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
            public string Insurer_A { get; set; }
            public string Insurer_A_NAIC { get; set; }
            public string Insurer_B { get; set; }
            public string Insurer_B_NAIC { get; set; }
            public string Insurer_C { get; set; }
            public string Insurer_C_NAIC { get; set; }
            public string Insurer_D { get; set; }
            public string Insurer_D_NAIC { get; set; }
            public string Insurer_E { get; set; }
            public string Insurer_E_NAIC { get; set; }
            public string Insurer_F { get; set; }
            public string Insurer_F_NAIC { get; set; }
        }

        internal class W9Doc
        {
            public string Text { get; set; }
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

                        var DateArea = new Rectangle(1910, 123, 290, 50); // (x, y, width, height)
                        var Date = Ocr.ReadPdf(FileLocation, DateArea).ToString();

                        var ProducerArea = new Rectangle(60, 480, 1100, 200); // (x, y, width, height)
                        var Producer = Ocr.ReadPdf(FileLocation, ProducerArea).ToString();

                        var InsuredArea = new Rectangle(60, 710, 1100, 200); // (x, y, width, height)
                        var Insured = Ocr.ReadPdf(FileLocation, InsuredArea).ToString();

                        var ContactNameArea = new Rectangle(1276, 440, 980, 50); // (x, y, width, height)
                        var ContactName = Ocr.ReadPdf(FileLocation, ContactNameArea).ToString();

                        var PhoneArea = new Rectangle(1310, 500, 480, 50); // (x, y, width, height)
                        var Phone = Ocr.ReadPdf(FileLocation, PhoneArea).ToString();

                        var FaxArea = new Rectangle(1930, 500, 280, 50); // (x, y, width, height)
                        var Fax = Ocr.ReadPdf(FileLocation, FaxArea).ToString();

                        var EmailArea = new Rectangle(1276, 549, 925, 50); // (x, y, width, height)
                        var Email = Ocr.ReadPdf(FileLocation, EmailArea).ToString();

                        var Insurer_AArea = new Rectangle(1305, 632, 700, 50); // (x, y, width, height)
                        var Insurer_A = Ocr.ReadPdf(FileLocation, Insurer_AArea).ToString();

                        var Insurer_A_NAICArea = new Rectangle(2000, 632, 210, 50); // (x, y, width, height)
                        var Insurer_A_NAIC = Ocr.ReadPdf(FileLocation, Insurer_A_NAICArea).ToString();

                        var Insurer_BArea = new Rectangle(1305, 680, 700, 50); // (x, y, width, height)
                        var Insurer_B = Ocr.ReadPdf(FileLocation, Insurer_BArea).ToString();

                        var Insurer_B_NAICArea = new Rectangle(2000, 680, 210, 50); // (x, y, width, height)
                        var Insurer_B_NAIC = Ocr.ReadPdf(FileLocation, Insurer_B_NAICArea).ToString();

                        var Insurer_CArea = new Rectangle(1305, 720, 700, 50); // (x, y, width, height)
                        var Insurer_C = Ocr.ReadPdf(FileLocation, Insurer_CArea).ToString();

                        var Insurer_C_NAICArea = new Rectangle(2000, 720, 210, 50); // (x, y, width, height)
                        var Insurer_C_NAIC = Ocr.ReadPdf(FileLocation, Insurer_C_NAICArea).ToString();

                        var Insurer_DArea = new Rectangle(1305, 765, 700, 50); // (x, y, width, height)
                        var Insurer_D = Ocr.ReadPdf(FileLocation, Insurer_DArea).ToString();

                        var Insurer_D_NAICArea = new Rectangle(2000, 765, 210, 50); // (x, y, width, height)
                        var Insurer_D_NAIC = Ocr.ReadPdf(FileLocation, Insurer_D_NAICArea).ToString();

                        var Insurer_EArea = new Rectangle(1305, 805, 700, 50); // (x, y, width, height)
                        var Insurer_E = Ocr.ReadPdf(FileLocation, Insurer_EArea).ToString();

                        var Insurer_E_NAICArea = new Rectangle(2000, 805, 210, 50); // (x, y, width, height)
                        var Insurer_E_NAIC = Ocr.ReadPdf(FileLocation, Insurer_E_NAICArea).ToString();

                        var Insurer_FArea = new Rectangle(1305, 850, 700, 50); // (x, y, width, height)
                        var Insurer_F = Ocr.ReadPdf(FileLocation, Insurer_FArea).ToString();

                        var Insurer_F_NAICArea = new Rectangle(2000, 850, 210, 50); // (x, y, width, height)
                        var Insurer_F_NAIC = Ocr.ReadPdf(FileLocation, Insurer_F_NAICArea).ToString();

                        // add to list and serialize it into JSON
                        OutputList.Add(new AcordDoc() {
                            Date = Date,
                            Producer = Producer,
                            Insured = Insured,
                            ContactName = ContactName,
                            Phone = Phone,
                            Fax = Fax,
                            Email = Email,
                            Insurer_A = Insurer_A,
                            Insurer_A_NAIC = Insurer_A_NAIC,
                            Insurer_B = Insurer_B,
                            Insurer_B_NAIC = Insurer_B_NAIC,
                            Insurer_C = Insurer_C,
                            Insurer_C_NAIC = Insurer_C_NAIC,
                            Insurer_D = Insurer_D,
                            Insurer_D_NAIC = Insurer_D_NAIC,
                            Insurer_F = Insurer_F,
                            Insurer_F_NAIC = Insurer_F_NAIC
                        });
                        var serializer = new JavaScriptSerializer();
                        var serializedOutputList = serializer.Serialize(OutputList);

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
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