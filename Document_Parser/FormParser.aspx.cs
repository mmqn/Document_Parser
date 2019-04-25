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
		internal class Result
		{
			public string Text { get; set; }
			public double Confidence { get; set; }
		}
		protected void UploadButton_Click(object sender, EventArgs e)
		{
			string selectedDocClass = DocumentClassDDL.SelectedValue;

            string fileName = FileUpload.PostedFile.FileName;
            string fileExtension = Path.GetExtension(fileName);

            // CHANGE: sets location file upload is saved in; currently set to user's desktop
            string fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            FileUpload.PostedFile.SaveAs(fileLocation);

            // extracts content from file upload for PDF reading
            var fileContent = FileUpload.FileContent;

			// if doc classification unspecified (accepts any doc)
			if (selectedDocClass == "unspecified")
			{
				if (FileUpload.HasFile)
				{
					if (fileExtension == ".pdf")
					{
						// initializes list to be serialized into JSON
						var OutputList = new List<Result>();

                        // initializes IronOCR's AdvancedOcr object
						var Ocr = new AdvancedOcr()
						{
							EnhanceResolution = true,
							EnhanceContrast = true,
							CleanBackgroundNoise = true,
							ColorDepth = 4,
							RotateAndStraighten = false,
							DetectWhiteTextOnDarkBackgrounds = false,
							ReadBarCodes = false,
							Language = IronOcr.Languages.English.OcrLanguagePack,
							Strategy = AdvancedOcr.OcrStrategy.Fast,
							ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
							InputImageType = AdvancedOcr.InputTypes.Document
						};


						var Result = Ocr.ReadPdf(fileContent);

                        // parse content by page, paragraph, or line
                        foreach (var page in Result.Pages)
						{
                            foreach (var paragraph in page.Paragraphs)
							{
								var paragraphText = paragraph.Text;
								double paragraphConfidence = Math.Round(paragraph.Confidence, 2);

                                // add `paragraphText` and `paragraphConfidence` to list
                                OutputList.Add(new Result() { Text = paragraphText, Confidence = paragraphConfidence });

                                // use this if we decide to parse text by line
                                //foreach (var line in paragraph.Lines)
                                //{
                                //    var lineText = line.Text;
                                //    double lineConfidence = line.Confidence;
                                //}
                            }
						}

						// serialize list into JSON
						var serializer = new JavaScriptSerializer();
						var serializedOutputList = serializer.Serialize(OutputList);

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
					}
					else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
					{
						var Ocr = new AdvancedOcr()
						{
							CleanBackgroundNoise = true,
							EnhanceContrast = true,
							EnhanceResolution = true,
							DetectWhiteTextOnDarkBackgrounds = true,
							RotateAndStraighten = true,
							ReadBarCodes = false,
							ColorDepth = 4,
							Language = IronOcr.Languages.English.OcrLanguagePack,
							Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
							ColorSpace = AdvancedOcr.OcrColorSpace.Color,
							InputImageType = AdvancedOcr.InputTypes.Document
						};

                        // this crop area is set to entire standard 8.5x11 document
                        var x = 0;
						var y = 0;
						var width = 2550;
						var height = 3300;
						var cropArea = new Rectangle(x, y, width, height);

						var Result = Ocr.Read(fileLocation, cropArea);
						var ResultText = Result.Text;
                        ResultTextArea.Text = ResultText.ToString();
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
			else if (selectedDocClass == "acord25")
			{
                if (FileUpload.HasFile)
                {
                    if (fileExtension == ".pdf")
                    {
                        // initializes list to be serialized into JSON
                        var OutputList = new List<Result>();

                        // initializes IronOCR's AdvancedOcr object
                        var Ocr = new AdvancedOcr()
                        {
                            EnhanceResolution = true,
                            EnhanceContrast = true,
                            CleanBackgroundNoise = true,
                            ColorDepth = 4,
                            RotateAndStraighten = false,
                            DetectWhiteTextOnDarkBackgrounds = false,
                            ReadBarCodes = false,
                            Language = IronOcr.Languages.English.OcrLanguagePack,
                            Strategy = AdvancedOcr.OcrStrategy.Fast,
                            ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                            InputImageType = AdvancedOcr.InputTypes.Document
                        };

                        // these settings currently retrieve second disclaimer textbox in form
                        var x = 50;
                        var y = 330;
                        var width = 2200;
                        var height = 115;
                        var CropArea = new Rectangle(x, y, width, height);
                        var Result = Ocr.ReadPdf(fileContent, CropArea);

                        // parse content by page, paragraph, or line
                        foreach (var page in Result.Pages)
                        {
                            foreach (var paragraph in page.Paragraphs)
                            {
                                var paragraphText = paragraph.Text;
                                double paragraphConfidence = Math.Round(paragraph.Confidence, 2);

                                // add `paragraphText` and `paragraphConfidence` to list
                                OutputList.Add(new Result() { Text = paragraphText, Confidence = paragraphConfidence });

                                // use this if we decide to parse text by line
                                //foreach (var line in paragraph.Lines)
                                //{
                                //    var lineText = line.Text;
                                //    double lineConfidence = line.Confidence;
                                //}
                            }
                        }

                        // serialize list into JSON
                        var serializer = new JavaScriptSerializer();
                        var serializedOutputList = serializer.Serialize(OutputList);

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
                    }
                    else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
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
            else if (selectedDocClass == "w9")
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