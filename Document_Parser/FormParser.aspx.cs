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
            public double Date_Confidence { get; set; }
            public string Producer { get; set; }
            public double Producer_Confidence { get; set; }
            public string Insured { get; set; }
            public double Insured_Confidence { get; set; }
            public string ContactName { get; set; }
            public double ContactName_Confidence { get; set; }
            public string Phone { get; set; }
            public double Phone_Confidence { get; set; }
            public string Fax { get; set; }
            public double Fax_Confidence { get; set; }
            public string Email { get; set; }
            public double Email_Confidence { get; set; }
            public string InsurerA { get; set; }
            public double InsurerA_Confidence { get; set; }
            public string InsurerANAIC { get; set; }
            public double InsurerANAIC_Confidence { get; set; }
            public string InsurerB { get; set; }
            public double InsurerB_Confidence { get; set; }
            public string InsurerBNAIC { get; set; }
            public double InsurerBNAIC_Confidence { get; set; }
            public string InsurerC { get; set; }
            public double InsurerC_Confidence { get; set; }
            public string InsurerCNAIC { get; set; }
            public double InsurerCNAIC_Confidence { get; set; }
            public string InsurerD { get; set; }
            public double InsurerD_Confidence { get; set; }
            public string InsurerDNAIC { get; set; }
            public double InsurerDNAIC_Confidence { get; set; }
            public string InsurerE { get; set; }
            public double InsurerE_Confidence { get; set; }
            public string InsurerENAIC { get; set; }
            public double InsurerENAIC_Confidence { get; set; }
            public string InsurerF { get; set; }
            public double InsurerF_Confidence { get; set; }
            public string InsurerFNAIC { get; set; }
            public double InsurerFNAIC_Confidence { get; set; }
            public string CertificateNumber { get; set; }
            public double CertificateNumber_Confidence { get; set; }
            public string RevisionNumber { get; set; }
            public double RevisionNumber_Confidence { get; set; }
            // insert left side of Commercial General Liability
            public string CGLEachOccurence { get; set; }
            public double CGLEachOccurence_Confidence { get; set; }
            public string CGLDamageToRentedPremises { get; set; }
            public double CGLDamageToRentedPremises_Confidence { get; set; }
            public string CGLMedExp { get; set; }
            public double CGLMedExp_Confidence { get; set; }
            public string CGLPersonalAndAdvInjury { get; set; }
            public double CGLPersonalAndAdvInjury_Confidence { get; set; }
            public string CGLGeneralAggregate { get; set; }
            public double CGLGeneralAggregate_Confidence { get; set; }
            public string CGLProductsCompOpAgg { get; set; }
            public double CGLProductsCompOpAgg_Confidence { get; set; }
            // insert left side of Automobile Liability
            public string ALCombinedSingleLimit { get; set; }
            public double ALCombinedSingleLimit_Confidence { get; set; }
            public string ALBodilyInjuryPerPerson { get; set; }
            public double ALBodilyInjuryPerPerson_Confidence { get; set; }
            public string ALBodilyInjuryPerAccident { get; set; }
            public double ALBodilyInjuryPerAccident_Confidence { get; set; }
            public string ALPropertyDamage { get; set; }
            public double ALPropertyDamage_Confidence { get; set; }
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
            if (FileUpload.HasFile)
            {
                FileUpload.PostedFile.SaveAs(FileLocation);
            }

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

                        var Date_Area = new Rectangle(1910, 123, 290, 50); // (x, y, width, height)
                        var Date_Obj = Ocr.ReadPdf(FileLocation, Date_Area);
                        var Date = Date_Obj.ToString();
                        var Date_Confidence = Math.Round(Date_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Producer_Area = new Rectangle(60, 480, 1100, 200); // (x, y, width, height)
                        var Producer_Obj = Ocr.ReadPdf(FileLocation, Producer_Area);
                        var Producer = Producer_Obj.ToString();
                        var Producer_Confidence = Math.Round(Producer_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Insured_Area = new Rectangle(60, 710, 1100, 200); // (x, y, width, height)
                        var Insured_Obj = Ocr.ReadPdf(FileLocation, Insured_Area);
                        var Insured = Insured_Obj.ToString();
                        var Insured_Confidence = Math.Round(Insured_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ContactName_Area = new Rectangle(1276, 440, 980, 50); // (x, y, width, height)
                        var ContactName_Obj = Ocr.ReadPdf(FileLocation, ContactName_Area);
                        var ContactName = ContactName_Obj.ToString();
                        var ContactName_Confidence = Math.Round(ContactName_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Phone_Area = new Rectangle(1310, 500, 480, 50); // (x, y, width, height)
                        var Phone_Obj = Ocr.ReadPdf(FileLocation, Phone_Area);
                        var Phone = Phone_Obj.ToString();
                        var Phone_Confidence= Math.Round(Phone_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Fax_Area = new Rectangle(1930, 500, 280, 50); // (x, y, width, height)
                        var Fax_Obj = Ocr.ReadPdf(FileLocation, Fax_Area);
                        var Fax = Fax_Obj.ToString();
                        var Fax_Confidence = Math.Round(Fax_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Email_Area = new Rectangle(1276, 549, 925, 50); // (x, y, width, height)
                        var Email_Obj = Ocr.ReadPdf(FileLocation, Email_Area);
                        var Email = Email_Obj.ToString();
                        var Email_Confidence = Math.Round(Email_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerA_Area = new Rectangle(1305, 632, 700, 50); // (x, y, width, height)
                        var InsurerA_Obj = Ocr.ReadPdf(FileLocation, InsurerA_Area);
                        var InsurerA = InsurerA_Obj.ToString();
                        var InsurerA_Confidence = Math.Round(InsurerA_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerANAIC_Area = new Rectangle(2000, 632, 210, 50); // (x, y, width, height)
                        var InsurerANAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerANAIC_Area);
                        var InsurerANAIC = InsurerANAIC_Obj.ToString();
                        var InsurerANAIC_Confidence = Math.Round(InsurerANAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerB_Area = new Rectangle(1305, 680, 700, 50); // (x, y, width, height)
                        var InsurerB_Obj = Ocr.ReadPdf(FileLocation, InsurerB_Area);
                        var InsurerB = InsurerB_Obj.ToString();
                        var InsurerB_Confidence = Math.Round(InsurerB_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerBNAIC_Area = new Rectangle(2000, 680, 210, 50); // (x, y, width, height)
                        var InsurerBNAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerBNAIC_Area);
                        var InsurerBNAIC = InsurerBNAIC_Obj.ToString();
                        var InsurerBNAIC_Confidence = Math.Round(InsurerBNAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerC_Area = new Rectangle(1305, 720, 700, 50); // (x, y, width, height)
                        var InsurerC_Obj = Ocr.ReadPdf(FileLocation, InsurerC_Area);
                        var InsurerC = InsurerC_Obj.ToString();
                        var InsurerC_Confidence = Math.Round(InsurerC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerCNAIC_Area = new Rectangle(2000, 720, 210, 50); // (x, y, width, height)
                        var InsurerCNAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerCNAIC_Area);
                        var InsurerCNAIC = InsurerCNAIC_Obj.ToString();
                        var InsurerCNAIC_Confidence = Math.Round(InsurerCNAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerD_Area = new Rectangle(1305, 765, 700, 50); // (x, y, width, height)
                        var InsurerD_Obj = Ocr.ReadPdf(FileLocation, InsurerD_Area);
                        var InsurerD = InsurerD_Obj.ToString();
                        var InsurerD_Confidence = Math.Round(InsurerD_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerDNAIC_Area = new Rectangle(2000, 765, 210, 50); // (x, y, width, height)
                        var InsurerDNAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerDNAIC_Area);
                        var InsurerDNAIC = InsurerDNAIC_Obj.ToString();
                        var InsurerDNAIC_Confidence = Math.Round(InsurerDNAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerE_Area = new Rectangle(1305, 805, 700, 50); // (x, y, width, height)
                        var InsurerE_Obj = Ocr.ReadPdf(FileLocation, InsurerE_Area);
                        var InsurerE = InsurerE_Obj.ToString();
                        var InsurerE_Confidence = Math.Round(InsurerE_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerENAIC_Area = new Rectangle(2000, 805, 210, 50); // (x, y, width, height)
                        var InsurerENAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerENAIC_Area);
                        var InsurerENAIC = InsurerENAIC_Obj.ToString();
                        var InsurerENAIC_Confidence = Math.Round(InsurerENAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerF_Area = new Rectangle(1305, 850, 700, 50); // (x, y, width, height)
                        var InsurerF_Obj = Ocr.ReadPdf(FileLocation, InsurerF_Area);
                        var InsurerF = InsurerF_Obj.ToString();
                        var InsurerF_Confidence = Math.Round(InsurerF_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var InsurerFNAIC_Area = new Rectangle(2000, 850, 210, 50); // (x, y, width, height)
                        var InsurerFNAIC_Obj = Ocr.ReadPdf(FileLocation, InsurerFNAIC_Area);
                        var InsurerFNAIC = InsurerFNAIC_Obj.ToString();
                        var InsurerFNAIC_Confidence = Math.Round(InsurerFNAIC_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CertificateNumber_Area = new Rectangle(980, 900, 600, 50); // (x, y, width, height)
                        var CertificateNumber_Obj = Ocr.ReadPdf(FileLocation, CertificateNumber_Area);
                        var CertificateNumber = CertificateNumber_Obj.ToString();
                        var CertificateNumber_Confidence = 0; //  Math.Round(CertificateNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var RevisionNumber_Area = new Rectangle(1900, 900, 500, 50); // (x, y, width, height)
                        var RevisionNumber_Obj = Ocr.ReadPdf(FileLocation, RevisionNumber_Area);
                        var RevisionNumber = RevisionNumber_Obj.ToString();
                        var RevisionNumber_Confidence = 0; // Math.Round(RevisionNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLEachOccurence_Area = new Rectangle(1930, 1119, 350, 50); // (x, y, width, height)
                        var CGLEachOccurence_Obj = Ocr.ReadPdf(FileLocation, CGLEachOccurence_Area);
                        var CGLEachOccurence = CGLEachOccurence_Obj.ToString();
                        var CGLEachOccurence_Confidence = Math.Round(CGLEachOccurence_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLDamageToRentedPremises_Area = new Rectangle(1930, 1165, 350, 50); // (x, y, width, height)
                        var CGLDamageToRentedPremises_Obj = Ocr.ReadPdf(FileLocation, CGLDamageToRentedPremises_Area);
                        var CGLDamageToRentedPremises = CGLDamageToRentedPremises_Obj.ToString();
                        var CGLDamageToRentedPremises_Confidence = Math.Round(CGLDamageToRentedPremises_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLMedExp_Area = new Rectangle(1930, 1205, 350, 50); // (x, y, width, height)
                        var CGLMedExp_Obj = Ocr.ReadPdf(FileLocation, CGLMedExp_Area);
                        var CGLMedExp = CGLMedExp_Obj.ToString();
                        var CGLMedExp_Confidence = Math.Round(CGLMedExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLPersonalAndAdvInjury_Area = new Rectangle(1930, 1250, 350, 50); // (x, y, width, height)
                        var CGLPersonalAndAdvInjury_Obj = Ocr.ReadPdf(FileLocation, CGLPersonalAndAdvInjury_Area);
                        var CGLPersonalAndAdvInjury = CGLPersonalAndAdvInjury_Obj.ToString();
                        var CGLPersonalAndAdvInjury_Confidence = Math.Round(CGLPersonalAndAdvInjury_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLGeneralAggregate_Area = new Rectangle(1930, 1295, 350, 50); // (x, y, width, height)
                        var CGLGeneralAggregate_Obj = Ocr.ReadPdf(FileLocation, CGLGeneralAggregate_Area);
                        var CGLGeneralAggregate = CGLGeneralAggregate_Obj.ToString();
                        var CGLGeneralAggregate_Confidence = Math.Round(CGLGeneralAggregate_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLProductsCompOpAgg_Area = new Rectangle(1930, 1340, 350, 50); // (x, y, width, height)
                        var CGLProductsCompOpAgg_Obj = Ocr.ReadPdf(FileLocation, CGLProductsCompOpAgg_Area);
                        var CGLProductsCompOpAgg = CGLProductsCompOpAgg_Obj.ToString();
                        var CGLProductsCompOpAgg_Confidence = Math.Round(CGLProductsCompOpAgg_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALCombinedSingleLimit_Area = new Rectangle(1930, 1440, 350, 50); // (x, y, width, height)
                        var ALCombinedSingleLimit_Obj = Ocr.ReadPdf(FileLocation, ALCombinedSingleLimit_Area);
                        var ALCombinedSingleLimit = ALCombinedSingleLimit_Obj.ToString();
                        var ALCombinedSingleLimit_Confidence = Math.Round(ALCombinedSingleLimit_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALBodilyInjuryPerPerson_Area = new Rectangle(1930, 1485, 350, 50); // (x, y, width, height)
                        var ALBodilyInjuryPerPerson_Obj = Ocr.ReadPdf(FileLocation, ALBodilyInjuryPerPerson_Area);
                        var ALBodilyInjuryPerPerson = ALBodilyInjuryPerPerson_Obj.ToString();
                        var ALBodilyInjuryPerPerson_Confidence = 0; // Math.Round(ALBodilyInjuryPerPerson_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALBodilyInjuryPerAccident_Area = new Rectangle(1930, 1540, 350, 50); // (x, y, width, height)
                        var ALBodilyInjuryPerAccident_Obj = Ocr.ReadPdf(FileLocation, ALBodilyInjuryPerAccident_Area);
                        var ALBodilyInjuryPerAccident = ALBodilyInjuryPerAccident_Obj.ToString();
                        var ALBodilyInjuryPerAccident_Confidence = Math.Round(ALBodilyInjuryPerAccident_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALPropertyDamage_Area = new Rectangle(1930, 1585, 350, 50); // (x, y, width, height)
                        var ALPropertyDamage_Obj = Ocr.ReadPdf(FileLocation, ALPropertyDamage_Area);
                        var ALPropertyDamage = ALPropertyDamage_Obj.ToString();
                        var ALPropertyDamage_Confidence = Math.Round(ALPropertyDamage_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        // add to list and serialize it into JSON
                        OutputList.Add(new AcordDoc() {
                            Date = Date,
                            Date_Confidence = Date_Confidence,
                            Producer = Producer,
                            Producer_Confidence = Producer_Confidence,
                            Insured = Insured,
                            Insured_Confidence = Insured_Confidence,
                            ContactName = ContactName,
                            ContactName_Confidence = ContactName_Confidence,
                            Phone = Phone,
                            Phone_Confidence = Phone_Confidence,
                            Fax = Fax,
                            Fax_Confidence = Fax_Confidence,
                            Email = Email,
                            Email_Confidence = Email_Confidence,
                            InsurerA = InsurerA,
                            InsurerA_Confidence = InsurerA_Confidence,
                            InsurerANAIC = InsurerANAIC,
                            InsurerANAIC_Confidence = InsurerANAIC_Confidence,
                            InsurerB = InsurerB,
                            InsurerB_Confidence = InsurerB_Confidence,
                            InsurerBNAIC = InsurerBNAIC,
                            InsurerBNAIC_Confidence = InsurerBNAIC_Confidence,
                            InsurerC = InsurerC,
                            InsurerC_Confidence = InsurerC_Confidence,
                            InsurerCNAIC = InsurerCNAIC,
                            InsurerCNAIC_Confidence = InsurerCNAIC_Confidence,
                            InsurerD = InsurerD,
                            InsurerD_Confidence = InsurerD_Confidence,
                            InsurerDNAIC = InsurerDNAIC,
                            InsurerDNAIC_Confidence = InsurerDNAIC_Confidence,
                            InsurerE = InsurerE,
                            InsurerE_Confidence = InsurerE_Confidence,
                            InsurerENAIC = InsurerENAIC,
                            InsurerENAIC_Confidence = InsurerENAIC_Confidence,
                            InsurerF = InsurerF,
                            InsurerF_Confidence = InsurerF_Confidence,
                            InsurerFNAIC = InsurerFNAIC,
                            InsurerFNAIC_Confidence = InsurerFNAIC_Confidence,
                            CertificateNumber = CertificateNumber,
                            CertificateNumber_Confidence = CertificateNumber_Confidence,
                            RevisionNumber = RevisionNumber,
                            RevisionNumber_Confidence = RevisionNumber_Confidence,
                            CGLEachOccurence = CGLEachOccurence,
                            CGLEachOccurence_Confidence = CGLEachOccurence_Confidence,
                            CGLDamageToRentedPremises = CGLDamageToRentedPremises,
                            CGLDamageToRentedPremises_Confidence = CGLDamageToRentedPremises_Confidence,
                            CGLMedExp = CGLMedExp,
                            CGLMedExp_Confidence = CGLMedExp_Confidence,
                            CGLPersonalAndAdvInjury = CGLPersonalAndAdvInjury,
                            CGLPersonalAndAdvInjury_Confidence = CGLPersonalAndAdvInjury_Confidence,
                            CGLGeneralAggregate = CGLGeneralAggregate,
                            CGLGeneralAggregate_Confidence = CGLGeneralAggregate_Confidence,
                            CGLProductsCompOpAgg = CGLProductsCompOpAgg,
                            CGLProductsCompOpAgg_Confidence = CGLProductsCompOpAgg_Confidence,
                            ALCombinedSingleLimit = ALCombinedSingleLimit,
                            ALCombinedSingleLimit_Confidence = ALCombinedSingleLimit_Confidence,
                            ALBodilyInjuryPerPerson = ALBodilyInjuryPerPerson,
                            ALBodilyInjuryPerPerson_Confidence = ALBodilyInjuryPerPerson_Confidence,
                            ALBodilyInjuryPerAccident = ALBodilyInjuryPerAccident,
                            ALBodilyInjuryPerAccident_Confidence = ALBodilyInjuryPerAccident_Confidence,
                            ALPropertyDamage = ALPropertyDamage,
                            ALPropertyDamage_Confidence = ALPropertyDamage_Confidence
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
				if (FileUpload.HasFile)
				{
                    if (FileType == ".pdf")
                    {

                    }
                    else if (FileType == ".png" || FileType == ".jpg" || FileType == ".jpeg")
                    {

                    }
                }
				else
				{
                    ResultTextArea.Text = "No file found.";
				}
			}
		}
	}
}
