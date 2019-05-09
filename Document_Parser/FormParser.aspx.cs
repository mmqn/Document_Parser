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
            // CGL: Commercial General Liability
            //public string CGLInsrLtr { get; set; }
            //public double CGLInsrLtr_Confidence { get; set; }
            public bool CGLCheckbox { get; set; }
            public double CGLCheckbox_Confidence { get; set; }
            //public bool CGLBothClaimsOccurCheckbox { get; set; }
            //public double CGLBothClaimsOccurCheckbox_Confidence { get; set; }
            //public bool CGLClaimsMadeCheckbox { get; set; }
            //public double CGLClaimsMadeCheckbox_Confidence { get; set; }
            public bool CGLOccurCheckbox { get; set; }
            public double CGLOccurCheckbox_Confidence { get; set; }
            public bool CGLOther1Checkbox { get; set; }
            public double CGLOther1Checkbox_Confidence { get; set; }
            public string CGLOther1Text { get; set; }
            public double CGLOther1Text_Confidence { get; set; }
            public bool CGLOther2Checkbox { get; set; }
            public double CGLOther2Checkbox_Confidence { get; set; }
            public string CGLOther2Text { get; set; }
            public double CGLOther2Text_Confidence { get; set; }
            public bool CGLLimitPolicyCheckbox { get; set; }
            public double CGLLimitPolicyCheckbox_Confidence { get; set; }
            public bool CGLLimitProjectCheckbox { get; set; }
            public double CGLLimitProjectCheckbox_Confidence { get; set; }
            //public bool CGLLimitLocCheckbox { get; set; }
            //public double CGLLimitLocCheckbox_Confidence { get; set; }
            public bool CGLLimitOtherCheckbox { get; set; }
            public double CGLLimitOtherCheckbox_Confidence { get; set; }
            public string CGLLimitOtherText { get; set; }
            public double CGLLimitOtherText_Confidence { get; set; }
            //public bool CGLAddlInsd { get; set; }
            //public double CGLAddlInsd_Confidence { get; set; }
            //public bool CGLSubrWvd { get; set; }
            //public double CGLSubrWvd_Confidence { get; set; }
            public string CGLPolicyNumber { get; set; }
            public double CGLPolicyNumber_Confidence { get; set; }
            public string CGLPolicyEff { get; set; }
            public double CGLPolicyEff_Confidence { get; set; }
            public string CGLPolicyExp { get; set; }
            public double CGLPolicyExp_Confidence { get; set; }
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
            // AL: Automobile Liability
            //public string ALInsrLtr { get; set; }
            //public double ALInsrLtr_Confidence { get; set; }
            public bool ALAnyAutoCheckbox { get; set; }
            public double ALAnyAutoCheckbox_Confidence { get; set; }
            public bool ALAllOwnedAutosCheckbox { get; set; }
            public double ALAllOwnedAutosCheckbox_Confidence { get; set; }
            public bool ALScheduledAutosCheckbox { get; set; }
            public double ALScheduledAutosCheckbox_Confidence { get; set; }
            public bool ALHiredAutosCheckbox { get; set; }
            public double ALHiredAutosCheckbox_Confidence { get; set; }
            public bool ALNonOwnedAutosCheckbox { get; set; }
            public double ALNonOwnedAutosCheckbox_Confidence { get; set; }
            //public bool ALAddlInsd { get; set; }
            //public double ALAddlInsd_Confidence { get; set; }
            //public bool ALSubrWvd { get; set; }
            //public double ALSubrWvd_Confidence { get; set; }
            public string ALPolicyNumber { get; set; }
            public double ALPolicyNumber_Confidence { get; set; }
            public string ALPolicyEff { get; set; }
            public double ALPolicyEff_Confidence { get; set; }
            public string ALPolicyExp { get; set; }
            public double ALPolicyExp_Confidence { get; set; }
            public string ALCombinedSingleLimit { get; set; }
            public double ALCombinedSingleLimit_Confidence { get; set; }
            public string ALBodilyInjuryPerPerson { get; set; }
            public double ALBodilyInjuryPerPerson_Confidence { get; set; }
            public string ALBodilyInjuryPerAccident { get; set; }
            public double ALBodilyInjuryPerAccident_Confidence { get; set; }
            public string ALPropertyDamage { get; set; }
            public double ALPropertyDamage_Confidence { get; set; }
            // UEL: Umbrella or Excess Liability
            //public string UELInsrLtr { get; set; }
            //public double UELInsrLtr_Confidence { get; set; }
            public bool UELUmbrellaLiabCheckbox { get; set; }
            public double UELUmbrellaLiabCheckbox_Confidence { get; set; }
            public bool UELExcessLiabCheckbox { get; set; }
            public double UELExcessLiabCheckbox_Confidence { get; set; }
            public bool UELOccurCheckbox { get; set; }
            public double UELOccurCheckbox_Confidence { get; set; }
            public bool UELClaimsMadeCheckbox { get; set; }
            public double UELClaimsMadeCheckbox_Confidence { get; set; }
            public bool UELDeductibleCheckbox { get; set; }
            public double UELDeductibleCheckbox_Confidence { get; set; }
            public bool UELRetentionCheckbox { get; set; }
            public double UELRetentionCheckbox_Confidence { get; set; }
            public string UELRetentionText { get; set; }
            public double UELRetentionText_Confidence { get; set; }
            //public bool UELAddlInsd { get; set; }
            //public double UELAddlInsd_Confidence { get; set; }
            //public bool UELSubrWvd { get; set; }
            //public double UELSubrWvd_Confidence { get; set; }
            public string UELPolicyNumber { get; set; }
            public double UELPolicyNumber_Confidence { get; set; }
            public string UELPolicyEff { get; set; }
            public double UELPolicyEff_Confidence { get; set; }
            public string UELPolicyExp { get; set; }
            public double UELPolicyExp_Confidence { get; set; }
            public string UELEachOccurence { get; set; }
            public double UELEachOccurence_Confidence { get; set; }
            public string UELAggregate { get; set; }
            public double UELAggregate_Confidence { get; set; }
            // WCEL: Workers Compensation and Employers' Liability
            //public bool WCELAnyMemberExcluded { get; set; }
            //public double WCELAnyMemberExcluded_Confidence { get; set; }
            //public bool WCELSubrWvd { get; set; }
            //public double WCELSubrWvd_Confidence { get; set; }
            public string WCELPolicyNumber { get; set; }
            public double WCELPolicyNumber_Confidence { get; set; }
            public string WCELPolicyEff { get; set; }
            public double WCELPolicyEff_Confidence { get; set; }
            public string WCELPolicyExp { get; set; }
            public double WCELPolicyExp_Confidence { get; set; }
            public bool WCELPerStatuteCheckbox { get; set; }
            public double WCELPerStatuteCheckbox_Confidence { get; set; }
            public bool WCELOtherLimitCheckbox { get; set; }
            public double WCELOtherLimitCheckbox_Confidence { get; set; }
            public string WCELPerStatuteOrOtherLimitText { get; set; }
            public double WCELPerStatuteOrOtherLimitText_Confidence { get; set; }
            public string WCELEachAccident { get; set; }
            public double WCELEachAccident_Confidence { get; set; }
            public string WCELDiseaseEachEmployee { get; set; }
            public double WCELDiseaseEachEmployee_Confidence { get; set; }
            public string WCELDiseasePolicyLimit { get; set; }
            public double WCELDiseasePolicyLimit_Confidence { get; set; }
            public string DescriptionOfOperations { get; set; }
            public double DescriptionOfOperations_Confidence { get; set; }
            public string CertificateHolder { get; set; }
            public double CertificateHolder_Confidence { get; set; }
            public string AuthorizedRepresentative { get; set; }
            public double AuthorizedRepresentative_Confidence { get; set; }
        }

        internal class W9Doc
        {
            public string Name { get; set; }
            public double Name_Confidence { get; set; }
            public string BusinessName { get; set; }
            public double BusinessName_Confidence { get; set; }
            public bool ClassificationIndividualCheckbox { get; set; }
            public double ClassificationIndividualCheckbox_Confidence { get; set; }
            public bool ClassificationCCorporationCheckbox { get; set; }
            public double ClassificationCCorporationCheckbox_Confidence { get; set; }
            public bool ClassificationSCorporationCheckbox { get; set; }
            public double ClassificationSCorporationCheckbox_Confidence { get; set; }
            public bool ClassificationPartnershipCheckbox { get; set; }
            public double ClassificationPartnershipCheckbox_Confidence { get; set; }
            public bool ClassificationTrustEstateCheckbox { get; set; }
            public double ClassificationTrustEstateCheckbox_Confidence { get; set; }
            public bool ClassificationLimitedLiabilityCheckbox { get; set; }
            public double ClassificationLimitedLiabilityCheckbox_Confidence { get; set; }
            public string ClassificationLimitedLiabilityCode { get; set; }
            public double ClassificationLimitedLiabilityCode_Confidence { get; set; }
            public bool ClassificationOtherCheckbox { get; set; }
            public double ClassificationOtherCheckbox_Confidence { get; set; }
            public string ClassificationOtherText { get; set; }
            public double ClassificationOtherText_Confidence { get; set; }
            public string ExemptPayeeCode { get; set; }
            public double ExemptPayeeCode_Confidence { get; set; }
            public string ExemptionFromFATCACode { get; set; }
            public double ExemptionFromFATCACode_Confidence { get; set; }
            public string Address { get; set; }
            public double Address_Confidence { get; set; }
            public string CityStateZip { get; set; }
            public double CityStateZip_Confidence { get; set; }
            public string RequesterNameAddress { get; set; }
            public double RequesterNameAddress_Confidence { get; set; }
            public string AccountNumbers { get; set; }
            public double AccountNumbers_Confidence { get; set; }
            public string SocialSecurity { get; set; }
            public double SocialSecurity_Confidence { get; set; }
            public string EmployerIdentification { get; set; }
            public double EmployerIdentification_Confidence { get; set; }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
		{
            IronOcr.License.LicenseKey = "IRONOCR-881455CC3C-338640-AD1406-5D3E070A56-74FA3CC-UExAEF09DFE54AE6D8-COMMUNITY.TRIAL.EXPIRES.06.JUN.2019";

            string SelectedDocType = DocumentClassDDL.SelectedValue;

            string FileName = FileUpload.PostedFile.FileName;
            string FileType = Path.GetExtension(FileName);

            // CHANGE: sets location file upload is saved in; currently set to user's desktop
            string FileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName);
            if (FileUpload.HasFile)
            {
                FileUpload.PostedFile.SaveAs(FileLocation);
            }

			// if doc type unspecified (accepts any doc)
			if (SelectedDocType == "unspecified")
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
                        var serializedOutputList = serializer.Serialize(OutputList); // final product; export this to database or wherever

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

            // if doc type is an ACORD 25 Form (typed)
			else if (SelectedDocType == "acord25")
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
                        var CertificateNumber_Confidence = Math.Round(CertificateNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var RevisionNumber_Area = new Rectangle(1900, 900, 500, 50); // (x, y, width, height)
                        var RevisionNumber_Obj = Ocr.ReadPdf(FileLocation, RevisionNumber_Area);
                        var RevisionNumber = RevisionNumber_Obj.ToString();
                        var RevisionNumber_Confidence = Math.Round(RevisionNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var CGLInsrLtr_Area = new Rectangle(); // (x, y, width, height)
                        //var CGLInsrLtr_Obj = Ocr.ReadPdf(FileLocation, CGLInsrLtr_Area);
                        //var CGLInsrLtr = CGLInsrLtr_Obj.ToString();
                        //var CGLInsrLtr_Confidence = Math.Round(CGLInsrLtr_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLCheckbox_Area = new Rectangle(135, 1117, 450, 50); // (x, y, width, height)
                        var CGLCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLCheckbox_Area);
                        var CGLCheckbox_Value = CGLCheckbox_Obj.ToString().Replace("COMMERCIAL GENERAL LIABILITY", "");
                        var CGLCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLCheckbox_Value)) { CGLCheckbox = true; }
                        var CGLCheckbox_Confidence = Math.Round(CGLCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var CGLBothClaimsOccurCheckbox_Area = new Rectangle(); // (x, y, width, height)
                        //var CGLBothClaimsOccurCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLBothClaimsOccurCheckbox_Area);
                        //var CGLBothClaimsOccurCheckbox = CGLBothClaimsOccurCheckbox_Obj.ToString();
                        //var CGLBothClaimsOccurCheckbox_Confidence = Math.Round(CGLBothClaimsOccurCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var CGLClaimsMadeCheckbox_Area = new Rectangle(); // (x, y, width, height)
                        //var CGLClaimsMadeCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLClaimsMadeCheckbox_Area);
                        //var CGLClaimsMadeCheckbox = CGLClaimsMadeCheckbox_Obj.ToString();
                        //var CGLClaimsMadeCheckbox_Confidence = Math.Round(CGLClaimsMadeCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLOccurCheckbox_Area = new Rectangle(412, 1160, 300, 50); // (x, y, width, height)
                        var CGLOccurCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLOccurCheckbox_Area);
                        var CGLOccurCheckbox_Value = CGLOccurCheckbox_Obj.ToString().Replace("OCCUR", "");
                        var CGLOccurCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLOccurCheckbox_Value)) { CGLOccurCheckbox = true; }
                        var CGLOccurCheckbox_Confidence = Math.Round(CGLOccurCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLOther1Text_Area = new Rectangle(190, 1200, 490, 70); // (x, y, width, height)
                        var CGLOther1Text_Obj = Ocr.ReadPdf(FileLocation, CGLOther1Text_Area);
                        var CGLOther1Text = CGLOther1Text_Obj.ToString();
                        var CGLOther1Text_Confidence = Math.Round(CGLOther1Text_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLOther1Checkbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLOther1Text)) { CGLOther1Checkbox = true; }
                        var CGLOther1Checkbox_Confidence = CGLOther1Text_Confidence;

                        var CGLOther2Text_Area = new Rectangle(195, 1250, 490, 70); // (x, y, width, height)
                        var CGLOther2Text_Obj = Ocr.ReadPdf(FileLocation, CGLOther2Text_Area);
                        var CGLOther2Text = CGLOther2Text_Obj.ToString();
                        var CGLOther2Text_Confidence = Math.Round(CGLOther2Text_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLOther2Checkbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLOther2Text)) { CGLOther2Checkbox = true; }
                        var CGLOther2Checkbox_Confidence = CGLOther1Text_Confidence;

                        var CGLLimitPolicyCheckbox_Area = new Rectangle(135, 1345, 150, 60); // (x, y, width, height)
                        var CGLLimitPolicyCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLLimitPolicyCheckbox_Area);
                        var CGLLimitPolicyCheckbox_Value = CGLLimitPolicyCheckbox_Obj.ToString().Replace("POLICY", "");
                        var CGLLimitPolicyCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLLimitPolicyCheckbox_Value)) { CGLLimitPolicyCheckbox = true; }
                        var CGLLimitPolicyCheckbox_Confidence = Math.Round(CGLLimitPolicyCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLLimitProjectCheckbox_Area = new Rectangle(276, 1340, 150, 50); // (x, y, width, height)
                        var CGLLimitProjectCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLLimitProjectCheckbox_Area);
                        var CGLLimitProjectCheckbox_Value = CGLLimitProjectCheckbox_Obj.ToString().Replace("PRO-", "").Replace("f", "").Replace("JECT", "");
                        var CGLLimitProjectCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLLimitProjectCheckbox_Value)) { CGLLimitProjectCheckbox = true; }
                        var CGLLimitProjectCheckbox_Confidence = Math.Round(CGLLimitProjectCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLLimitLocCheckbox_Area = new Rectangle(); // (x, y, width, height)
                        var CGLLimitLocCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLLimitLocCheckbox_Area);
                        var CGLLimitLocCheckbox = CGLLimitLocCheckbox_Obj.ToString();
                        var CGLLimitLocCheckbox_Confidence = Math.Round(CGLLimitLocCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLLimitOtherCheckbox_Area = new Rectangle(); // (x, y, width, height)
                        var CGLLimitOtherCheckbox_Obj = Ocr.ReadPdf(FileLocation, CGLLimitOtherCheckbox_Area);
                        var CGLLimitOtherCheckbox_Value = CGLLimitOtherCheckbox_Obj.ToString().Replace("OTHER:", "");
                        var CGLLimitOtherCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(CGLLimitOtherCheckbox_Value)) { CGLLimitOtherCheckbox = true; }
                        var CGLLimitOtherCheckbox_Confidence = Math.Round(CGLLimitOtherCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLLimitOtherText_Area = new Rectangle(295, 1385, 370, 55); // (x, y, width, height)
                        var CGLLimitOtherText_Obj = Ocr.ReadPdf(FileLocation, CGLLimitOtherText_Area);
                        var CGLLimitOtherText = CGLLimitOtherText_Obj.ToString();
                        var CGLLimitOtherText_Confidence = Math.Round(CGLLimitOtherText_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var CGLAddlInsd_Area = new Rectangle(); // (x, y, width, height)
                        //var CGLAddlInsd_Obj = Ocr.ReadPdf(FileLocation, CGLAddlInsd_Area);
                        //var CGLAddlInsd = CGLAddlInsd_Obj.ToString();
                        //var CGLAddlInsd_Confidence = Math.Round(CGLAddlInsd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var CGLSubrWvd_Area = new Rectangle(); // (x, y, width, height)
                        //var CGLSubrWvd_Obj = Ocr.ReadPdf(FileLocation, CGLSubrWvd_Area);
                        //var CGLSubrWvd = CGLSubrWvd_Obj.ToString();
                        //var CGLSubrWvd_Confidence = Math.Round(CGLSubrWvd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLPolicyNumber_Area = new Rectangle(805, 1119, 425, 340); // (x, y, width, height)
                        var CGLPolicyNumber_Obj = Ocr.ReadPdf(FileLocation, CGLPolicyNumber_Area);
                        var CGLPolicyNumber = CGLPolicyNumber_Obj.ToString();
                        var CGLPolicyNumber_Confidence = Math.Round(CGLPolicyNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLPolicyEff_Area = new Rectangle(1255, 1119, 180, 340); // (x, y, width, height)
                        var CGLPolicyEff_Obj = Ocr.ReadPdf(FileLocation, CGLPolicyEff_Area);
                        var CGLPolicyEff = CGLPolicyEff_Obj.ToString();
                        var CGLPolicyEff_Confidence = Math.Round(CGLPolicyEff_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLPolicyExp_Area = new Rectangle(1410, 1119, 180, 340); // (x, y, width, height)
                        var CGLPolicyExp_Obj = Ocr.ReadPdf(FileLocation, CGLPolicyExp_Area);
                        var CGLPolicyExp = CGLPolicyExp_Obj.ToString();
                        var CGLPolicyExp_Confidence = Math.Round(CGLPolicyExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLEachOccurence_Area = new Rectangle(1960, 1119, 320, 50); // (x, y, width, height)
                        var CGLEachOccurence_Obj = Ocr.ReadPdf(FileLocation, CGLEachOccurence_Area);
                        var CGLEachOccurence = CGLEachOccurence_Obj.ToString();
                        var CGLEachOccurence_Confidence = Math.Round(CGLEachOccurence_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLDamageToRentedPremises_Area = new Rectangle(1960, 1165, 320, 50); // (x, y, width, height)
                        var CGLDamageToRentedPremises_Obj = Ocr.ReadPdf(FileLocation, CGLDamageToRentedPremises_Area);
                        var CGLDamageToRentedPremises = CGLDamageToRentedPremises_Obj.ToString();
                        var CGLDamageToRentedPremises_Confidence = Math.Round(CGLDamageToRentedPremises_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLMedExp_Area = new Rectangle(1960, 1212, 260, 50); // (x, y, width, height)
                        var CGLMedExp_Obj = Ocr.ReadPdf(FileLocation, CGLMedExp_Area);
                        var CGLMedExp = CGLMedExp_Obj.ToString();
                        var CGLMedExp_Confidence = Math.Round(CGLMedExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLPersonalAndAdvInjury_Area = new Rectangle(1960, 1255, 260, 50); // (x, y, width, height)
                        var CGLPersonalAndAdvInjury_Obj = Ocr.ReadPdf(FileLocation, CGLPersonalAndAdvInjury_Area);
                        var CGLPersonalAndAdvInjury = CGLPersonalAndAdvInjury_Obj.ToString();
                        var CGLPersonalAndAdvInjury_Confidence = Math.Round(CGLPersonalAndAdvInjury_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLGeneralAggregate_Area = new Rectangle(1950, 1293, 350, 50); // (x, y, width, height)
                        var CGLGeneralAggregate_Obj = Ocr.ReadPdf(FileLocation, CGLGeneralAggregate_Area);
                        var CGLGeneralAggregate = CGLGeneralAggregate_Obj.ToString();
                        var CGLGeneralAggregate_Confidence = Math.Round(CGLGeneralAggregate_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CGLProductsCompOpAgg_Area = new Rectangle(1960, 1350, 265, 50); // (x, y, width, height)
                        var CGLProductsCompOpAgg_Obj = Ocr.ReadPdf(FileLocation, CGLProductsCompOpAgg_Area);
                        var CGLProductsCompOpAgg = CGLProductsCompOpAgg_Obj.ToString();
                        var CGLProductsCompOpAgg_Confidence = Math.Round(CGLProductsCompOpAgg_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var ALInsrLtr_Area = new Rectangle(); // (x, y, width, height)
                        //var ALInsrLtr_Obj = Ocr.ReadPdf(FileLocation, ALInsrLtr_Area);
                        //var ALInsrLtr = ALInsrLtr_Obj.ToString();
                        //var ALInsrLtr_Confidence = Math.Round(ALInsrLtr_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALAnyAutoCheckbox_Area = new Rectangle(135, 1485, 190, 50); // (x, y, width, height)
                        var ALAnyAutoCheckbox_Obj = Ocr.ReadPdf(FileLocation, ALAnyAutoCheckbox_Area);
                        var ALAnyAutoCheckbox_Value = ALAnyAutoCheckbox_Obj.ToString().Replace("OTHER:", "");
                        var ALAnyAutoCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ALAnyAutoCheckbox_Value)) { ALAnyAutoCheckbox = true; }
                        var ALAnyAutoCheckbox_Confidence = Math.Round(ALAnyAutoCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALAllOwnedAutosCheckbox_Area = new Rectangle(131, 1528, 215, 55); // (x, y, width, height)
                        var ALAllOwnedAutosCheckbox_Obj = Ocr.ReadPdf(FileLocation, ALAllOwnedAutosCheckbox_Area);
                        var ALAllOwnedAutosCheckbox_Value = ALAllOwnedAutosCheckbox_Obj.ToString().Replace("ALL OWNED", "").Replace("AUTOS", "");
                        var ALAllOwnedAutosCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ALAllOwnedAutosCheckbox_Value)) { ALAllOwnedAutosCheckbox = true; }
                        var ALAllOwnedAutosCheckbox_Confidence = Math.Round(ALAllOwnedAutosCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALScheduledAutosCheckbox_Area = new Rectangle(391, 1530, 220, 52); // (x, y, width, height)
                        var ALScheduledAutosCheckbox_Obj = Ocr.ReadPdf(FileLocation, ALScheduledAutosCheckbox_Area);
                        var ALScheduledAutosCheckbox_Value = ALScheduledAutosCheckbox_Obj.ToString().Replace("SCHEDULED", "").Replace("AUTOS", "");
                        var ALScheduledAutosCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ALScheduledAutosCheckbox_Value)) { ALScheduledAutosCheckbox = true; }
                        var ALScheduledAutosCheckbox_Confidence = Math.Round(ALScheduledAutosCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALHiredAutosCheckbox_Area = new Rectangle(135, 1575, 225, 50); // (x, y, width, height)
                        var ALHiredAutosCheckbox_Obj = Ocr.ReadPdf(FileLocation, ALHiredAutosCheckbox_Area);
                        var ALHiredAutosCheckbox_Value = ALHiredAutosCheckbox_Obj.ToString().Replace("HIRED AUTOS", "");
                        var ALHiredAutosCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ALHiredAutosCheckbox_Value)) { ALHiredAutosCheckbox = true; }
                        var ALHiredAutosCheckbox_Confidence = Math.Round(ALHiredAutosCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALNonOwnedAutosCheckbox_Area = new Rectangle(395, 1573, 220, 50); // (x, y, width, height)
                        var ALNonOwnedAutosCheckbox_Obj = Ocr.ReadPdf(FileLocation, ALNonOwnedAutosCheckbox_Area);
                        var ALNonOwnedAutosCheckbox_Value = ALNonOwnedAutosCheckbox_Obj.ToString().Replace("NON-OWNED ", "").Replace("AUTOS", "");
                        var ALNonOwnedAutosCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ALNonOwnedAutosCheckbox_Value)) { ALNonOwnedAutosCheckbox = true; }
                        var ALNonOwnedAutosCheckbox_Confidence = Math.Round(ALNonOwnedAutosCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var ALAddlInsd_Area = new Rectangle(); // (x, y, width, height)
                        //var ALAddlInsd_Obj = Ocr.ReadPdf(FileLocation, ALAddlInsd_Area);
                        //var ALAddlInsd = ALAddlInsd_Obj.ToString();
                        //var ALAddlInsd_Confidence = Math.Round(ALAddlInsd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var ALSubrWvd_Area = new Rectangle(); // (x, y, width, height)
                        //var ALSubrWvd_Obj = Ocr.ReadPdf(FileLocation, ALSubrWvd_Area);
                        //var ALSubrWvd = ALSubrWvd_Obj.ToString();
                        //var ALSubrWvd_Confidence = Math.Round(ALSubrWvd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALPolicyNumber_Area = new Rectangle(779, 1410, 438, 225); // (x, y, width, height)
                        var ALPolicyNumber_Obj = Ocr.ReadPdf(FileLocation, ALPolicyNumber_Area);
                        var ALPolicyNumber = ALPolicyNumber_Obj.ToString();
                        var ALPolicyNumber_Confidence = Math.Round(ALPolicyNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALPolicyEff_Area = new Rectangle(1217, 1410, 180, 225); // (x, y, width, height)
                        var ALPolicyEff_Obj = Ocr.ReadPdf(FileLocation, ALPolicyEff_Area);
                        var ALPolicyEff = ALPolicyEff_Obj.ToString();
                        var ALPolicyEff_Confidence = Math.Round(ALPolicyEff_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALPolicyExp_Area = new Rectangle(1400, 1410, 180, 225); // (x, y, width, height)
                        var ALPolicyExp_Obj = Ocr.ReadPdf(FileLocation, ALPolicyExp_Area);
                        var ALPolicyExp = ALPolicyExp_Obj.ToString();
                        var ALPolicyExp_Confidence = Math.Round(ALPolicyExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALCombinedSingleLimit_Area = new Rectangle(1965, 1445, 255, 40); // (x, y, width, height)
                        var ALCombinedSingleLimit_Obj = Ocr.ReadPdf(FileLocation, ALCombinedSingleLimit_Area);
                        var ALCombinedSingleLimit = ALCombinedSingleLimit_Obj.ToString();
                        var ALCombinedSingleLimit_Confidence = Math.Round(ALCombinedSingleLimit_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALBodilyInjuryPerPerson_Area = new Rectangle(1965, 1485, 259, 47); // (x, y, width, height)
                        var ALBodilyInjuryPerPerson_Obj = Ocr.ReadPdf(FileLocation, ALBodilyInjuryPerPerson_Area);
                        var ALBodilyInjuryPerPerson = ALBodilyInjuryPerPerson_Obj.ToString();
                        var ALBodilyInjuryPerPerson_Confidence = Math.Round(ALBodilyInjuryPerPerson_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALBodilyInjuryPerAccident_Area = new Rectangle(1965, 1535, 258, 45); // (x, y, width, height)
                        var ALBodilyInjuryPerAccident_Obj = Ocr.ReadPdf(FileLocation, ALBodilyInjuryPerAccident_Area);
                        var ALBodilyInjuryPerAccident = ALBodilyInjuryPerAccident_Obj.ToString();
                        var ALBodilyInjuryPerAccident_Confidence = Math.Round(ALBodilyInjuryPerAccident_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ALPropertyDamage_Area = new Rectangle(1965, 1575, 259, 50); // (x, y, width, height)
                        var ALPropertyDamage_Obj = Ocr.ReadPdf(FileLocation, ALPropertyDamage_Area);
                        var ALPropertyDamage = ALPropertyDamage_Obj.ToString();
                        var ALPropertyDamage_Confidence = Math.Round(ALPropertyDamage_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var UELInsrLtr_Area = new Rectangle(); // (x, y, width, height)
                        //var UELInsrLtr_Obj = Ocr.ReadPdf(FileLocation, UELInsrLtr_Area);
                        //var UELInsrLtr = UELInsrLtr_Obj.ToString();
                        //var UELInsrLtr_Confidence = Math.Round(UELInsrLtr_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELUmbrellaLiabCheckbox_Area = new Rectangle(135, 1670, 265, 45); // (x, y, width, height)
                        var UELUmbrellaLiabCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELUmbrellaLiabCheckbox_Area);
                        var UELUmbrellaLiabCheckbox_Value = UELUmbrellaLiabCheckbox_Obj.ToString().Replace("UMBRELLA LIAB", "");
                        var UELUmbrellaLiabCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELUmbrellaLiabCheckbox_Value)) { UELUmbrellaLiabCheckbox = true; }
                        var UELUmbrellaLiabCheckbox_Confidence = Math.Round(UELUmbrellaLiabCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELExcessLiabCheckbox_Area = new Rectangle(135, 1715, 230, 45); // (x, y, width, height)
                        var UELExcessLiabCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELExcessLiabCheckbox_Area);
                        var UELExcessLiabCheckbox_Value = UELExcessLiabCheckbox_Obj.ToString().Replace("EXCESS LIAB", "");
                        var UELExcessLiabCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELExcessLiabCheckbox_Value)) { UELExcessLiabCheckbox = true; }
                        var UELExcessLiabCheckbox_Confidence = Math.Round(UELExcessLiabCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELOccurCheckbox_Area = new Rectangle(435, 1670, 170, 45); // (x, y, width, height)
                        var UELOccurCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELOccurCheckbox_Area);
                        var UELOccurCheckbox_Value = UELOccurCheckbox_Obj.ToString().Replace("OCCUR", "");
                        var UELOccurCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELOccurCheckbox_Value)) { UELOccurCheckbox = true; }
                        var UELOccurCheckbox_Confidence = Math.Round(UELOccurCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELClaimsMadeCheckbox_Area = new Rectangle(435, 1715, 230, 45); // (x, y, width, height)
                        var UELClaimsMadeCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELClaimsMadeCheckbox_Area);
                        var UELClaimsMadeCheckbox_Value = UELClaimsMadeCheckbox_Obj.ToString().Replace("CLAIMS-MADE", "");
                        var UELClaimsMadeCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELClaimsMadeCheckbox_Value)) { UELClaimsMadeCheckbox = true; }
                        var UELClaimsMadeCheckbox_Confidence = Math.Round(UELClaimsMadeCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELDeductibleCheckbox_Area = new Rectangle(135, 1760, 135, 40); // (x, y, width, height)
                        var UELDeductibleCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELDeductibleCheckbox_Area);
                        var UELDeductibleCheckbox_Value = UELDeductibleCheckbox_Obj.ToString().Replace("DED", "");
                        var UELDeductibleCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELDeductibleCheckbox_Value)) { UELDeductibleCheckbox = true; }
                        var UELDeductibleCheckbox_Confidence = Math.Round(UELDeductibleCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELRetentionCheckbox_Area = new Rectangle(270, 1760, 200, 40); // (x, y, width, height)
                        var UELRetentionCheckbox_Obj = Ocr.ReadPdf(FileLocation, UELRetentionCheckbox_Area);
                        var UELRetentionCheckbox_Value = UELRetentionCheckbox_Obj.ToString().Replace("RETENTION", "");
                        var UELRetentionCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(UELRetentionCheckbox_Value)) { UELRetentionCheckbox = true; }
                        var UELRetentionCheckbox_Confidence = Math.Round(UELRetentionCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELRetentionText_Area = new Rectangle(495, 1750, 165, 57); // (x, y, width, height)
                        var UELRetentionText_Obj = Ocr.ReadPdf(FileLocation, UELRetentionText_Area);
                        var UELRetentionText = UELRetentionText_Obj.ToString();
                        var UELRetentionText_Confidence = Math.Round(UELRetentionText_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var UELAddlInsd_Area = new Rectangle(); // (x, y, width, height)
                        //var UELAddlInsd_Obj = Ocr.ReadPdf(FileLocation, UELAddlInsd_Area);
                        //var UELAddlInsd = UELAddlInsd_Obj.ToString();
                        //var UELAddlInsd_Confidence = Math.Round(UELAddlInsd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        //var UELSubrWvd_Area = new Rectangle(); // (x, y, width, height)
                        //var UELSubrWvd_Obj = Ocr.ReadPdf(FileLocation, UELSubrWvd_Area);
                        //var UELSubrWvd = UELSubrWvd_Obj.ToString();
                        //var UELSubrWvd_Confidence = Math.Round(UELSubrWvd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELPolicyNumber_Area = new Rectangle(795, 1670, 450, 1305); // (x, y, width, height)
                        var UELPolicyNumber_Obj = Ocr.ReadPdf(FileLocation, UELPolicyNumber_Area);
                        var UELPolicyNumber = UELPolicyNumber_Obj.ToString();
                        var UELPolicyNumber_Confidence = Math.Round(UELPolicyNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELPolicyEff_Area = new Rectangle(1240, 1670, 175, 135); // (x, y, width, height)
                        var UELPolicyEff_Obj = Ocr.ReadPdf(FileLocation, UELPolicyEff_Area);
                        var UELPolicyEff = UELPolicyEff_Obj.ToString();
                        var UELPolicyEff_Confidence = Math.Round(UELPolicyEff_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELPolicyExp_Area = new Rectangle(1415, 1670, 180, 135); // (x, y, width, height)
                        var UELPolicyExp_Obj = Ocr.ReadPdf(FileLocation, UELPolicyExp_Area);
                        var UELPolicyExp = UELPolicyExp_Obj.ToString();
                        var UELPolicyExp_Confidence = Math.Round(UELPolicyExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELEachOccurence_Area = new Rectangle(1965, 1670, 300, 50); // (x, y, width, height)
                        var UELEachOccurence_Obj = Ocr.ReadPdf(FileLocation, UELEachOccurence_Area);
                        var UELEachOccurence = UELEachOccurence_Obj.ToString();
                        var UELEachOccurence_Confidence = Math.Round(UELEachOccurence_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var UELAggregate_Area = new Rectangle(1965, 1715, 230, 50); // (x, y, width, height)
                        var UELAggregate_Obj = Ocr.ReadPdf(FileLocation, UELAggregate_Area);
                        var UELAggregate = UELAggregate_Obj.ToString();
                        var UELAggregate_Confidence = Math.Round(UELAggregate_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        // bool WCELAnyMemberExcluded if 'N', return false, if 'Y', return true
                        // double WCELAnyMemberExcluded_Confidence

                        //var WCELSubrWvd_Area = new Rectangle(); // (x, y, width, height)
                        //var WCELSubrWvd_Obj = Ocr.ReadPdf(FileLocation, WCELSubrWvd_Area);
                        //var WCELSubrWvd = WCELSubrWvd_Obj.ToString();
                        //var WCELSubrWvd_Confidence = Math.Round(WCELSubrWvd_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELPolicyNumber_Area = new Rectangle(800, 1805, 440, 175); // (x, y, width, height)
                        var WCELPolicyNumber_Obj = Ocr.ReadPdf(FileLocation, WCELPolicyNumber_Area);
                        var WCELPolicyNumber = WCELPolicyNumber_Obj.ToString();
                        var WCELPolicyNumber_Confidence = Math.Round(WCELPolicyNumber_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELPolicyEff_Area = new Rectangle(1245, 1805, 170, 175); // (x, y, width, height)
                        var WCELPolicyEff_Obj = Ocr.ReadPdf(FileLocation, WCELPolicyEff_Area);
                        var WCELPolicyEff = WCELPolicyEff_Obj.ToString();
                        var WCELPolicyEff_Confidence = Math.Round(WCELPolicyEff_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELPolicyExp_Area = new Rectangle(1415, 1805, 185, 175); // (x, y, width, height)
                        var WCELPolicyExp_Obj = Ocr.ReadPdf(FileLocation, WCELPolicyExp_Area);
                        var WCELPolicyExp = WCELPolicyExp_Obj.ToString();
                        var WCELPolicyExp_Confidence = Math.Round(WCELPolicyExp_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELPerStatuteCheckbox_Area = new Rectangle(1595, 1800, 190, 45); // (x, y, width, height)
                        var WCELPerStatuteCheckbox_Obj = Ocr.ReadPdf(FileLocation, WCELPerStatuteCheckbox_Area);
                        var WCELPerStatuteCheckbox_Value = WCELPerStatuteCheckbox_Obj.ToString().Replace("PER", "").Replace("STATUTE", "");
                        var WCELPerStatuteCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(WCELPerStatuteCheckbox_Value)) { WCELPerStatuteCheckbox = true; }
                        var WCELPerStatuteCheckbox_Confidence = Math.Round(WCELPerStatuteCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELOtherLimitCheckbox_Area = new Rectangle(1780, 1800, 138, 43); // (x, y, width, height)
                        var WCELOtherLimitCheckbox_Obj = Ocr.ReadPdf(FileLocation, WCELOtherLimitCheckbox_Area);
                        var WCELOtherLimitCheckbox_Value = WCELOtherLimitCheckbox_Obj.ToString().Replace("OTH-", "").Replace("ER", "");
                        var WCELOtherLimitCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(WCELOtherLimitCheckbox_Value)) { WCELOtherLimitCheckbox = true; }
                        var WCELOtherLimitCheckbox_Confidence = Math.Round(WCELOtherLimitCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELPerStatuteOrOtherLimitText_Area = new Rectangle(1935, 1805, 285, 40); // (x, y, width, height)
                        var WCELPerStatuteOrOtherLimitText_Obj = Ocr.ReadPdf(FileLocation, WCELPerStatuteOrOtherLimitText_Area);
                        var WCELPerStatuteOrOtherLimitText = WCELPerStatuteOrOtherLimitText_Obj.ToString();
                        var WCELPerStatuteOrOtherLimitText_Confidence = Math.Round(WCELPerStatuteOrOtherLimitText_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELEachAccident_Area = new Rectangle(1960, 1845, 255, 40); // (x, y, width, height)
                        var WCELEachAccident_Obj = Ocr.ReadPdf(FileLocation, WCELEachAccident_Area);
                        var WCELEachAccident = WCELEachAccident_Obj.ToString();
                        var WCELEachAccident_Confidence = Math.Round(WCELEachAccident_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELDiseaseEachEmployee_Area = new Rectangle(1960, 1890, 255, 45); // (x, y, width, height)
                        var WCELDiseaseEachEmployee_Obj = Ocr.ReadPdf(FileLocation, WCELDiseaseEachEmployee_Area);
                        var WCELDiseaseEachEmployee = WCELDiseaseEachEmployee_Obj.ToString();
                        var WCELDiseaseEachEmployee_Confidence = Math.Round(WCELDiseaseEachEmployee_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var WCELDiseasePolicyLimit_Area = new Rectangle(1960, 1940, 255, 45); // (x, y, width, height)
                        var WCELDiseasePolicyLimit_Obj = Ocr.ReadPdf(FileLocation, WCELDiseasePolicyLimit_Area);
                        var WCELDiseasePolicyLimit = WCELDiseasePolicyLimit_Obj.ToString();
                        var WCELDiseasePolicyLimit_Confidence = Math.Round(WCELDiseasePolicyLimit_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var DescriptionOfOperations_Area = new Rectangle(70, 2155, 2155, 280); // (x, y, width, height)
                        var DescriptionOfOperations_Obj = Ocr.ReadPdf(FileLocation, DescriptionOfOperations_Area);
                        var DescriptionOfOperations = DescriptionOfOperations_Obj.ToString();
                        var DescriptionOfOperations_Confidence = Math.Round(DescriptionOfOperations_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CertificateHolder_Area = new Rectangle(65, 2475, 1085, 315); // (x, y, width, height)
                        var CertificateHolder_Obj = Ocr.ReadPdf(FileLocation, CertificateHolder_Area);
                        var CertificateHolder = CertificateHolder_Obj.ToString();
                        var CertificateHolder_Confidence = Math.Round(CertificateHolder_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var AuthorizedRepresentative_Area = new Rectangle(1150, 2695, 1080, 95); // (x, y, width, height)
                        var AuthorizedRepresentative_Obj = Ocr.ReadPdf(FileLocation, AuthorizedRepresentative_Area);
                        var AuthorizedRepresentative = AuthorizedRepresentative_Obj.ToString();
                        var AuthorizedRepresentative_Confidence = Math.Round(AuthorizedRepresentative_Obj.Pages[0].Paragraphs[0].Confidence, 2);

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
                            //CGLInsrLtr = CGLInsrLtr,
                            //CGLInsrLtr_Confidence = CGLInsrLtr_Confidence,
                            CGLCheckbox = CGLCheckbox,
                            CGLCheckbox_Confidence = CGLCheckbox_Confidence,
                            //CGLBothClaimsOccurCheckbox = CGLBothClaimsOccurCheckbox,
                            //CGLBothClaimsOccurCheckbox_Confidence = CGLBothClaimsOccurCheckbox_Confidence,
                            //CGLClaimsMadeCheckbox = CGLClaimsMadeCheckbox,
                            //CGLClaimsMadeCheckbox_Confidence = CGLClaimsMadeCheckbox_Confidence,
                            CGLOccurCheckbox = CGLOccurCheckbox,
                            CGLOccurCheckbox_Confidence = CGLOccurCheckbox_Confidence,
                            CGLOther1Checkbox = CGLOther1Checkbox,
                            CGLOther1Checkbox_Confidence = CGLOther1Checkbox_Confidence,
                            CGLOther1Text = CGLOther1Text,
                            CGLOther1Text_Confidence = CGLOther1Text_Confidence,
                            CGLOther2Checkbox = CGLOther2Checkbox,
                            CGLOther2Checkbox_Confidence = CGLOther2Checkbox_Confidence,
                            CGLOther2Text = CGLOther2Text,
                            CGLOther2Text_Confidence = CGLOther2Text_Confidence,
                            CGLLimitPolicyCheckbox = CGLLimitPolicyCheckbox,
                            CGLLimitPolicyCheckbox_Confidence = CGLLimitPolicyCheckbox_Confidence,
                            CGLLimitProjectCheckbox = CGLLimitProjectCheckbox,
                            CGLLimitProjectCheckbox_Confidence = CGLLimitProjectCheckbox_Confidence,
                            //CGLLimitLocCheckbox = CGLLimitLocCheckbox,
                            //CGLLimitLocCheckbox_Confidence = CGLLimitLocCheckbox_Confidence,
                            CGLLimitOtherCheckbox = CGLLimitOtherCheckbox,
                            CGLLimitOtherCheckbox_Confidence = CGLLimitOtherCheckbox_Confidence,
                            CGLLimitOtherText = CGLLimitOtherText,
                            CGLLimitOtherText_Confidence = CGLLimitOtherText_Confidence,
                            //CGLAddlInsd = CGLAddlInsd,
                            //CGLAddlInsd_Confidence = CGLAddlInsd_Confidence,
                            //CGLSubrWvd = CGLSubrWvd,
                            //CGLSubrWvd_Confidence = CGLSubrWvd_Confidence,
                            CGLPolicyNumber = CGLPolicyNumber,
                            CGLPolicyNumber_Confidence = CGLPolicyNumber_Confidence,
                            CGLPolicyEff = CGLPolicyEff,
                            CGLPolicyEff_Confidence = CGLPolicyEff_Confidence,
                            CGLPolicyExp = CGLPolicyExp,
                            CGLPolicyExp_Confidence = CGLPolicyExp_Confidence,
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
                            //ALInsrLtr = ALInsrLtr,
                            //ALInsrLtr_Confidence = ALInsrLtr_Confidence,
                            ALAnyAutoCheckbox = ALAnyAutoCheckbox,
                            ALAnyAutoCheckbox_Confidence = ALAnyAutoCheckbox_Confidence,
                            ALAllOwnedAutosCheckbox = ALAllOwnedAutosCheckbox,
                            ALAllOwnedAutosCheckbox_Confidence = ALAllOwnedAutosCheckbox_Confidence,
                            ALScheduledAutosCheckbox = ALScheduledAutosCheckbox,
                            ALScheduledAutosCheckbox_Confidence = ALScheduledAutosCheckbox_Confidence,
                            ALHiredAutosCheckbox = ALHiredAutosCheckbox,
                            ALHiredAutosCheckbox_Confidence = ALHiredAutosCheckbox_Confidence,
                            ALNonOwnedAutosCheckbox = ALNonOwnedAutosCheckbox,
                            ALNonOwnedAutosCheckbox_Confidence = ALNonOwnedAutosCheckbox_Confidence,
                            //ALAddlInsd = ALAddlInsd,
                            //ALAddlInsd_Confidence = ALAddlInsd_Confidence,
                            //ALSubrWvd = ALSubrWvd,
                            //ALSubrWvd_Confidence = ALSubrWvd_Confidence,
                            ALPolicyNumber = ALPolicyNumber,
                            ALPolicyNumber_Confidence = ALPolicyNumber_Confidence,
                            ALPolicyEff = ALPolicyEff,
                            ALPolicyEff_Confidence = ALPolicyEff_Confidence,
                            ALPolicyExp = ALPolicyExp,
                            ALPolicyExp_Confidence = ALPolicyExp_Confidence,
                            ALCombinedSingleLimit = ALCombinedSingleLimit,
                            ALCombinedSingleLimit_Confidence = ALCombinedSingleLimit_Confidence,
                            ALBodilyInjuryPerPerson = ALBodilyInjuryPerPerson,
                            ALBodilyInjuryPerPerson_Confidence = ALBodilyInjuryPerPerson_Confidence,
                            ALBodilyInjuryPerAccident = ALBodilyInjuryPerAccident,
                            ALBodilyInjuryPerAccident_Confidence = ALBodilyInjuryPerAccident_Confidence,
                            ALPropertyDamage = ALPropertyDamage,
                            ALPropertyDamage_Confidence = ALPropertyDamage_Confidence,
                            //UELInsrLtr = UELInsrLtr,
                            //UELInsrLtr_Confidence = UELInsrLtr_Confidence,
                            UELUmbrellaLiabCheckbox = UELUmbrellaLiabCheckbox,
                            UELUmbrellaLiabCheckbox_Confidence = UELUmbrellaLiabCheckbox_Confidence,
                            UELExcessLiabCheckbox = UELExcessLiabCheckbox,
                            UELExcessLiabCheckbox_Confidence = UELExcessLiabCheckbox_Confidence,
                            UELOccurCheckbox = UELOccurCheckbox,
                            UELOccurCheckbox_Confidence = UELOccurCheckbox_Confidence,
                            UELClaimsMadeCheckbox = UELClaimsMadeCheckbox,
                            UELClaimsMadeCheckbox_Confidence = UELClaimsMadeCheckbox_Confidence,
                            UELDeductibleCheckbox = UELDeductibleCheckbox,
                            UELDeductibleCheckbox_Confidence = UELDeductibleCheckbox_Confidence,
                            UELRetentionCheckbox = UELRetentionCheckbox,
                            UELRetentionCheckbox_Confidence = UELRetentionCheckbox_Confidence,
                            UELRetentionText = UELRetentionText,
                            UELRetentionText_Confidence = UELRetentionText_Confidence,
                            //UELAddlInsd = UELAddlInsd,
                            //UELAddlInsd_Confidence = UELAddlInsd_Confidence,
                            //UELSubrWvd = UELSubrWvd,
                            //UELSubrWvd_Confidence = UELSubrWvd_Confidence,
                            UELPolicyNumber = UELPolicyNumber,
                            UELPolicyNumber_Confidence = UELPolicyNumber_Confidence,
                            UELPolicyEff = UELPolicyEff,
                            UELPolicyEff_Confidence = UELPolicyEff_Confidence,
                            UELPolicyExp = UELPolicyExp,
                            UELPolicyExp_Confidence = UELPolicyExp_Confidence,
                            UELEachOccurence = UELEachOccurence,
                            UELEachOccurence_Confidence = UELEachOccurence_Confidence,
                            UELAggregate = UELAggregate,
                            UELAggregate_Confidence = UELAggregate_Confidence,
                            //WCELAnyMemberExcluded = WCELAnyMemberExcluded,
                            //WCELAnyMemberExcluded_Confidence = WCELAnyMemberExcluded_Confidence,
                            //WCELSubrWvd = WCELSubrWvd,
                            //WCELSubrWvd_Confidence = WCELSubrWvd_Confidence,
                            WCELPolicyNumber = WCELPolicyNumber,
                            WCELPolicyNumber_Confidence = WCELPolicyNumber_Confidence,
                            WCELPolicyEff = WCELPolicyEff,
                            WCELPolicyEff_Confidence = WCELPolicyEff_Confidence,
                            WCELPolicyExp = WCELPolicyExp,
                            WCELPolicyExp_Confidence = WCELPolicyExp_Confidence,
                            WCELPerStatuteCheckbox = WCELPerStatuteCheckbox,
                            WCELPerStatuteCheckbox_Confidence = WCELPerStatuteCheckbox_Confidence,
                            WCELOtherLimitCheckbox = WCELOtherLimitCheckbox,
                            WCELOtherLimitCheckbox_Confidence = WCELOtherLimitCheckbox_Confidence,
                            WCELPerStatuteOrOtherLimitText = WCELPerStatuteOrOtherLimitText,
                            WCELPerStatuteOrOtherLimitText_Confidence = WCELPerStatuteOrOtherLimitText_Confidence,
                            WCELEachAccident = WCELEachAccident,
                            WCELEachAccident_Confidence = WCELEachAccident_Confidence,
                            WCELDiseaseEachEmployee = WCELDiseaseEachEmployee,
                            WCELDiseaseEachEmployee_Confidence = WCELDiseaseEachEmployee_Confidence,
                            WCELDiseasePolicyLimit = WCELDiseasePolicyLimit,
                            WCELDiseasePolicyLimit_Confidence = WCELDiseasePolicyLimit_Confidence,
                            DescriptionOfOperations = DescriptionOfOperations,
                            DescriptionOfOperations_Confidence = DescriptionOfOperations_Confidence,
                            CertificateHolder = CertificateHolder,
                            CertificateHolder_Confidence = CertificateHolder_Confidence,
                            AuthorizedRepresentative = AuthorizedRepresentative,
                            AuthorizedRepresentative_Confidence = AuthorizedRepresentative_Confidence
                        });
                        var serializer = new JavaScriptSerializer();
                        var serializedOutputList = serializer.Serialize(OutputList); // final product; export this to database or wherever

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

            // if doc type is a W9 Form (handwritten)
            else if (SelectedDocType == "w9")
			{
				if (FileUpload.HasFile)
				{
                    if (FileType == ".pdf")
                    {
                        // initializes list to be serialized into JSON
                        var OutputList = new List<W9Doc>();

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

                        var PageOne = new[] { 1 }; // only the first page of W-9's have fields to read

                        var Name_Area = new Rectangle(200, 341, 1950, 73); // (x, y, width, height)
                        var Name_Obj = Ocr.ReadPdf(FileLocation, Name_Area, PageOne);
                        var Name = Name_Obj.ToString();
                        var Name_Confidence = Math.Round(Name_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var BusinessName_Area = new Rectangle(200, 436, 1949, 73); // (x, y, width, height)
                        var BusinessName_Obj = Ocr.ReadPdf(FileLocation, BusinessName_Area, PageOne);
                        var BusinessName = BusinessName_Obj.ToString();
                        var BusinessName_Confidence = Math.Round(BusinessName_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationIndividualCheckbox_Area = new Rectangle(251, 598, 159, 25); // (x, y, width, height)
                        var ClassificationIndividualCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationIndividualCheckbox_Area, PageOne);
                        var ClassificationIndividualCheckbox_Value = ClassificationIndividualCheckbox_Obj.ToString().Replace("l Individl ml", "");
                        var ClassificationIndividualCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationIndividualCheckbox_Value)) { ClassificationIndividualCheckbox = true; }
                        var ClassificationIndividualCheckbox_Confidence = Math.Round(ClassificationIndividualCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationCCorporationCheckbox_Area = new Rectangle(679, 598, 132, 20); // (x, y, width, height)
                        var ClassificationCCorporationCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationCCorporationCheckbox_Area, PageOne);
                        var ClassificationCCorporationCheckbox_Value = ClassificationCCorporationCheckbox_Obj.ToString().Replace("I C. Corn,", "");
                        var ClassificationCCorporationCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationCCorporationCheckbox_Value)) { ClassificationCCorporationCheckbox = true; }
                        var ClassificationCCorporationCheckbox_Confidence = Math.Round(ClassificationCCorporationCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationSCorporationCheckbox_Area = new Rectangle(949, 598, 132, 20); // (x, y, width, height)
                        var ClassificationSCorporationCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationSCorporationCheckbox_Area, PageOne);
                        var ClassificationSCorporationCheckbox_Value = ClassificationSCorporationCheckbox_Obj.ToString().Replace("I St Cnrnr", "");
                        var ClassificationSCorporationCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationSCorporationCheckbox_Value)) { ClassificationSCorporationCheckbox = true; }
                        var ClassificationSCorporationCheckbox_Confidence = Math.Round(ClassificationSCorporationCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationPartnershipCheckbox_Area = new Rectangle(1218, 598, 132, 20); // (x, y, width, height)
                        var ClassificationPartnershipCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationPartnershipCheckbox_Area, PageOne);
                        var ClassificationPartnershipCheckbox_Value = ClassificationPartnershipCheckbox_Obj.ToString().Replace("I p9nrtrvarr", "");
                        var ClassificationPartnershipCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationPartnershipCheckbox_Value)) { ClassificationPartnershipCheckbox = true; }
                        var ClassificationPartnershipCheckbox_Confidence = Math.Round(ClassificationPartnershipCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationTrustEstateCheckbox_Area = new Rectangle(1488, 598, 132, 20); // (x, y, width, height)
                        var ClassificationTrustEstateCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationTrustEstateCheckbox_Area, PageOne);
                        var ClassificationTrustEstateCheckbox_Value = ClassificationTrustEstateCheckbox_Obj.ToString().Replace("I Tn Ief/p:", "");
                        var ClassificationTrustEstateCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationTrustEstateCheckbox_Value)) { ClassificationTrustEstateCheckbox = true; }
                        var ClassificationTrustEstateCheckbox_Confidence = Math.Round(ClassificationTrustEstateCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationLimitedLiabilityCheckbox_Area = new Rectangle(251, 688, 137, 20); // (x, y, width, height)
                        var ClassificationLimitedLiabilityCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationLimitedLiabilityCheckbox_Area, PageOne);
                        var ClassificationLimitedLiabilityCheckbox_Value = ClassificationLimitedLiabilityCheckbox_Obj.ToString().Replace("l I imifnri", "");
                        var ClassificationLimitedLiabilityCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationLimitedLiabilityCheckbox_Value)) { ClassificationLimitedLiabilityCheckbox = true; }
                        var ClassificationLimitedLiabilityCheckbox_Confidence = Math.Round(ClassificationLimitedLiabilityCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationLimitedLiabilityCode_Area = new Rectangle(1340, 670, 330, 50); // (x, y, width, height)
                        var ClassificationLimitedLiabilityCode_Obj = Ocr.ReadPdf(FileLocation, ClassificationLimitedLiabilityCode_Area, PageOne);
                        var ClassificationLimitedLiabilityCode = ClassificationLimitedLiabilityCode_Obj.ToString().Replace("P=Partnership) _ ", "").Replace(" ", "");
                        var ClassificationLimitedLiabilityCode_Confidence = Math.Round(ClassificationLimitedLiabilityCode_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationOtherCheckbox_Area = new Rectangle(251, 688, 117, 20); // (x, y, width, height)
                        var ClassificationOtherCheckbox_Obj = Ocr.ReadPdf(FileLocation, ClassificationOtherCheckbox_Area, PageOne);
                        var ClassificationOtherCheckbox_Value = ClassificationOtherCheckbox_Obj.ToString().Replace("l I imih:", "");
                        var ClassificationOtherCheckbox = false;
                        if (!String.IsNullOrWhiteSpace(ClassificationOtherCheckbox_Value)) { ClassificationOtherCheckbox = true; }
                        var ClassificationOtherCheckbox_Confidence = Math.Round(ClassificationOtherCheckbox_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ClassificationOtherText_Area = new Rectangle(602, 851, 1096, 40); // (x, y, width, height)
                        var ClassificationOtherText_Obj = Ocr.ReadPdf(FileLocation, ClassificationOtherText_Area, PageOne);
                        var ClassificationOtherText = ClassificationOtherText_Obj.ToString();
                        var ClassificationOtherText_Confidence = Math.Round(ClassificationOtherText_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ExemptPayeeCode_Area = new Rectangle(2035, 626, 115, 73); // (x, y, width, height)
                        var ExemptPayeeCode_Obj = Ocr.ReadPdf(FileLocation, ExemptPayeeCode_Area, PageOne);
                        var ExemptPayeeCode = ExemptPayeeCode_Obj.ToString();
                        var ExemptPayeeCode_Confidence = Math.Round(ExemptPayeeCode_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var ExemptionFromFATCACode_Area = new Rectangle(1889, 762, 261, 73); // (x, y, width, height)
                        var ExemptionFromFATCACode_Obj = Ocr.ReadPdf(FileLocation, ExemptionFromFATCACode_Area, PageOne);
                        var ExemptionFromFATCACode = ExemptionFromFATCACode_Obj.ToString();
                        var ExemptionFromFATCACode_Confidence = Math.Round(ExemptionFromFATCACode_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var Address_Area = new Rectangle(225, 932, 1228, 40); // (x, y, width, height)
                        var Address_Obj = Ocr.ReadPdf(FileLocation, Address_Area, PageOne);
                        var Address = Address_Obj.ToString();
                        var Address_Confidence = Math.Round(Address_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var CityStateZip_Area = new Rectangle(225, 1025, 1228, 40); // (x, y, width, height)
                        var CityStateZip_Obj = Ocr.ReadPdf(FileLocation, CityStateZip_Area, PageOne);
                        var CityStateZip = CityStateZip_Obj.ToString();
                        var CityStateZip_Confidence = Math.Round(CityStateZip_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var RequesterNameAddress_Area = new Rectangle(1470, 935, 683, 128); // (x, y, width, height)
                        var RequesterNameAddress_Obj = Ocr.ReadPdf(FileLocation, RequesterNameAddress_Area, PageOne);
                        var RequesterNameAddress = RequesterNameAddress_Obj.ToString();
                        var RequesterNameAddress_Confidence = Math.Round(RequesterNameAddress_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var AccountNumbers_Area = new Rectangle(200, 1104, 1950, 73); // (x, y, width, height)
                        var AccountNumbers_Obj = Ocr.ReadPdf(FileLocation, AccountNumbers_Area, PageOne);
                        var AccountNumbers = AccountNumbers_Obj.ToString();
                        var AccountNumbers_Confidence = Math.Round(AccountNumbers_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var SocialSecurity_Area = new Rectangle(1550, 1271, 600, 70); // (x, y, width, height)
                        var SocialSecurity_Obj = Ocr.ReadPdf(FileLocation, SocialSecurity_Area, PageOne);
                        var SocialSecurity = SocialSecurity_Obj.ToString().Replace("l", "").Replace("-", "").Replace(" ", "");
                        var SocialSecurity_Confidence = Math.Round(SocialSecurity_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        var EmployerIdentification_Area = new Rectangle(1550, 1450, 601, 69); // (x, y, width, height)
                        var EmployerIdentification_Obj = Ocr.ReadPdf(FileLocation, EmployerIdentification_Area, PageOne);
                        var EmployerIdentification = EmployerIdentification_Obj.ToString().Replace("l", "").Replace("-", "").Replace(" ", "");
                        var EmployerIdentification_Confidence = Math.Round(EmployerIdentification_Obj.Pages[0].Paragraphs[0].Confidence, 2);

                        // add to list and serialize it into JSON
                        OutputList.Add(new W9Doc() {
                            Name = Name,
                            Name_Confidence = Name_Confidence,
                            BusinessName = BusinessName,
                            BusinessName_Confidence = BusinessName_Confidence,
                            ClassificationIndividualCheckbox = ClassificationIndividualCheckbox,
                            ClassificationIndividualCheckbox_Confidence = ClassificationIndividualCheckbox_Confidence,
                            ClassificationCCorporationCheckbox = ClassificationCCorporationCheckbox,
                            ClassificationCCorporationCheckbox_Confidence = ClassificationCCorporationCheckbox_Confidence,
                            ClassificationSCorporationCheckbox = ClassificationSCorporationCheckbox,
                            ClassificationSCorporationCheckbox_Confidence = ClassificationSCorporationCheckbox_Confidence,
                            ClassificationPartnershipCheckbox = ClassificationPartnershipCheckbox,
                            ClassificationPartnershipCheckbox_Confidence = ClassificationPartnershipCheckbox_Confidence,
                            ClassificationTrustEstateCheckbox = ClassificationTrustEstateCheckbox,
                            ClassificationTrustEstateCheckbox_Confidence = ClassificationTrustEstateCheckbox_Confidence,
                            ClassificationLimitedLiabilityCheckbox = ClassificationLimitedLiabilityCheckbox,
                            ClassificationLimitedLiabilityCheckbox_Confidence = ClassificationLimitedLiabilityCheckbox_Confidence,
                            ClassificationLimitedLiabilityCode = ClassificationLimitedLiabilityCode,
                            ClassificationLimitedLiabilityCode_Confidence = ClassificationLimitedLiabilityCode_Confidence,
                            ClassificationOtherCheckbox = ClassificationOtherCheckbox,
                            ClassificationOtherCheckbox_Confidence = ClassificationOtherCheckbox_Confidence,
                            ClassificationOtherText = ClassificationOtherText,
                            ClassificationOtherText_Confidence = ClassificationOtherText_Confidence,
                            ExemptPayeeCode = ExemptPayeeCode,
                            ExemptPayeeCode_Confidence = ExemptPayeeCode_Confidence,
                            ExemptionFromFATCACode = ExemptionFromFATCACode,
                            ExemptionFromFATCACode_Confidence = ExemptionFromFATCACode_Confidence,
                            Address = Address,
                            Address_Confidence = Address_Confidence,
                            CityStateZip = CityStateZip,
                            CityStateZip_Confidence = CityStateZip_Confidence,
                            RequesterNameAddress = RequesterNameAddress,
                            RequesterNameAddress_Confidence = RequesterNameAddress_Confidence,
                            AccountNumbers = AccountNumbers,
                            AccountNumbers_Confidence = AccountNumbers_Confidence,
                            SocialSecurity = SocialSecurity,
                            SocialSecurity_Confidence = SocialSecurity_Confidence,
                            EmployerIdentification = EmployerIdentification,
                            EmployerIdentification_Confidence = EmployerIdentification_Confidence
                        });
                        var serializer = new JavaScriptSerializer();
                        var serializedOutputList = serializer.Serialize(OutputList); // final product; export this to database or wherever

                        // place JSON object in text area HTML element
                        ResultTextArea.Text = serializedOutputList;
                    }
                    else if (FileType == ".png" || FileType == ".jpg" || FileType == ".jpeg")
                    {
                        // TODO: Image W-9 files
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
