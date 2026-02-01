using EducationalPlatform.Application.DTOs.Certificate;
using EducationalPlatform.Application.Interfaces.External_services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services.External_services
{

    /// <summary>
    /// MatterHub certificate PDF generator using QuestPDF
    /// Generates professional, branded certificates for MatterHub platform
    /// </summary>
    public class MatterHubCertificateGenerator : IMatterHubCertificateGenerator
    {
        // MatterHub Branding
        private const string OrganizationName = "MatterHub";
        private const string CertificateTitle = "CERTIFICATE OF COMPLETION";

        // MatterHub Color Scheme (Cream, Navy, Gold)
        private static readonly string DarkNavy = "#1A1A2E";
        private static readonly string Gold = "#D4AF37";
        private static readonly string SoftGold = "#C9A227";
        private static readonly string Cream = "#FFF8F0";
        private static readonly string CourseBoxBg = "#F5EDE0";
        private static readonly string MidGray = "#6B6B6B";
        private static readonly string LightGray = "#A8A8A8";
        private static readonly string DarkText = "#2C2C2C";
        private static readonly string DividerColor = "#DDD0B8";
        private static readonly string FooterTextColor = "#A89060";

        public MatterHubCertificateGenerator()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<string> GenerateCertificatePdfAsync(
            CertificateDetailsDto CertificateDetailsDto,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                var directory = System.IO.Path.GetDirectoryName(CertificateDetailsDto.PdfFilePath);
                if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(0);
                        page.DefaultTextStyle(x => x.FontSize(12).FontColor(DarkText));
                        page.Content().Element(c => ComposeCertificate(c, CertificateDetailsDto));
                    });
                })
                .GeneratePdf(CertificateDetailsDto.PdfFilePath);

            }, cancellationToken);

            return CertificateDetailsDto.PdfFilePath;
        }

        public async Task<byte[]> GenerateCertificatePdfBytesAsync(
            CertificateDetailsDto CertificateDetailsDto,
            CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(0);
                        page.DefaultTextStyle(x => x.FontSize(12).FontColor(DarkText));
                        page.Content().Element(c => ComposeCertificate(c, CertificateDetailsDto));
                    });
                })
                .GeneratePdf();
            }, cancellationToken);
        }

        // =====================================================================
        // MAIN LAYOUT
        // =====================================================================
        private void ComposeCertificate(IContainer container, CertificateDetailsDto certificate)
        {
            container
                .Background(Cream)
                .Column(column =>
                {
                    // Top navy bar
                    column.Item().Height(52).Element(c => ComposeTopBar(c));

                    // Main content area
                    column.Item().Element(c => ComposeMainContent(c, certificate));

                    // Bottom navy bar
                    column.Item().Height(52).Element(c => ComposeBottomBar(c, certificate));
                });
        }

        // =====================================================================
        // TOP BAR — dark navy with gold "EXCELLENCE IN LEARNING" text
        // =====================================================================
        private void ComposeTopBar(IContainer container)
        {
            container
                .Background(DarkNavy)
                .PaddingTop(16)

                .AlignCenter()
                .Text("✦  EXCELLENCE IN LEARNING  ✦")
                .FontSize(9)
                .FontColor(Gold);
        }

        // =====================================================================
        // BOTTOM BAR — dark navy with copyright text
        // =====================================================================
        private void ComposeBottomBar(IContainer container, CertificateDetailsDto certificate)
        {
            container
                .Background(DarkNavy)
                .AlignCenter()
                .PaddingTop(16)
                .Text($"© {DateTime.Now.Year} MatterHub — All Rights Reserved  |  Scan to Verify")
                .FontSize(7.5f)
                .Italic()
                .FontColor(FooterTextColor);
        }

        // =====================================================================
        // MAIN CONTENT — double gold border wrapping everything inside
        // =====================================================================
        private void ComposeMainContent(IContainer container, CertificateDetailsDto certificate)
        {
            container
                .Padding(22) // outer margin from page edge to outer border
                .Border(2.5f)
                .BorderColor(Gold)
                .Padding(7)  // gap between outer and inner border
                .Border(0.8f)
                .BorderColor(Gold)
                .PaddingTop(28)
                .PaddingBottom(20)
                .PaddingLeft(40)
                .PaddingRight(40)
                .Column(column =>
                {
                    column.Spacing(0);

                    // Title: CERTIFICATE OF COMPLETION
                    column.Item().Element(ComposeTitle);

                    // Gold divider with center diamond
                    column.Item().PaddingTop(10).Element(ComposeGoldDivider);

                    // "This is to certify that"
                    column.Item().PaddingTop(22).Element(c =>
                    {
                        c.AlignCenter()
                            .Text("This is to certify that")
                            .FontSize(11)
                            .Italic()
                            .FontColor(MidGray);
                    });

                    // Recipient name + gold underline
                    column.Item().PaddingTop(18).Element(c => ComposeName(c, certificate));

                    // "has successfully completed the course"
                    column.Item().PaddingTop(14).Element(c =>
                    {
                        c.AlignCenter()
                            .Text("has successfully completed the course")
                            .FontSize(11)
                            .Italic()
                            .FontColor(MidGray);
                    });

                    // Course title box
                    column.Item().PaddingTop(18).Element(c => ComposeCourseBox(c, certificate));

                    // Award date
                    column.Item().PaddingTop(16).Element(c =>
                    {
                        c.AlignCenter()
                            .Text($"Awarded on {certificate.IssuedAt:MMMM dd, yyyy}")
                            .FontSize(10)
                            .FontColor(MidGray);
                    });

                    // Thin divider line
                    column.Item().PaddingTop(18).Element(c =>
                    {
                        c.AlignCenter()
                            .Width(360)
                            .Height(0.6f)
                            .Background(DividerColor);
                    });

                    // Footer: signature (left) + details (right)
                    column.Item().PaddingTop(14).Element(c => ComposeFooter(c, certificate));
                });
        }

        // =====================================================================
        // TITLE
        // =====================================================================
        private void ComposeTitle(IContainer container)
        {
            container
                .AlignCenter()
                .Text(CertificateTitle)
                .FontSize(11)
                .Bold()
                .FontColor(DarkNavy);
        }

        // =====================================================================
        // GOLD DIVIDER — line with a diamond in the center
        //   QuestPDF can't draw arbitrary shapes, so we simulate:
        //   [line] [small gold square rotated via padding trick] [line]
        // =====================================================================
        private void ComposeGoldDivider(IContainer container)
        {
            container.AlignCenter().Row(row =>
            {
                // Left line segment
                row.RelativeItem().AlignCenter().Height(1.5f).Background(Gold);

                // Center diamond (approximated as a small gold square)
                row.ConstantItem(10).AlignCenter().AlignMiddle()
                    .Width(7).Height(7)
                    .Background(Gold);

                // Right line segment
                row.RelativeItem().AlignCenter().Height(1.5f).Background(Gold);
            });
        }

        // =====================================================================
        // RECIPIENT NAME + soft gold underline
        // =====================================================================
        private void ComposeName(IContainer container, CertificateDetailsDto certificate)
        {
            container.AlignCenter().Column(col =>
            {
                col.Item()
                    .AlignCenter()
                    .Text(certificate.UserFullName)
                    .FontSize(GetAdaptiveFontSize(certificate.UserFullName, 36, 26))
                    .Bold()
                    .FontColor(DarkNavy);

                // Soft gold underline
                col.Item().PaddingTop(6).AlignCenter()
                    .Width(180)
                    .Height(1)
                    .Background(SoftGold);
            });
        }

        // =====================================================================
        // COURSE TITLE BOX — rounded bg with gold left accent bar
        // =====================================================================
        private void ComposeCourseBox(IContainer container, CertificateDetailsDto certificate)
        {
            container.AlignCenter().Width(440).Row(row =>
            {
                // Gold left accent bar
                row.ConstantItem(5).Background(Gold);

                // Course text on light warm background
                row.RelativeItem()
                    .Background(CourseBoxBg)
                    .PaddingVertical(14)
                    .PaddingHorizontal(20)
                    .AlignCenter()
                    .Text(certificate.CourseTitle)
                    .FontSize(GetAdaptiveFontSize(certificate.CourseTitle, 17, 13))
                    .Bold()
                    .FontColor(DarkNavy);
            });
        }

        // =====================================================================
        // FOOTER — signature on the left, certificate details on the right
        // =====================================================================
        private void ComposeFooter(IContainer container, CertificateDetailsDto certificate)
        {
            container.Row(row =>
            {
                // --- Left: Signature ---
                row.RelativeItem().Column(sigCol =>
                {
                    sigCol.Spacing(4);

                    // Organization name (italic, acts as stylised signature)
                    sigCol.Item().AlignCenter()
                        .Text(certificate.InstructorName ?? OrganizationName)
                        .FontSize(14)
                        .Italic()
                        .FontColor(DarkNavy);

                    // Signature line
                    sigCol.Item().AlignCenter()
                        .Width(120)
                        .Height(1)
                        .Background(DarkNavy);

                    // Labels
                    sigCol.Item().PaddingTop(6).AlignCenter()
                        .Text("Authorized Signatory")
                        .FontSize(8.5f)
                        .FontColor(MidGray);

                    sigCol.Item().AlignCenter()
                        .Text("MatterHub Administration")
                        .FontSize(8.5f)
                        .FontColor(MidGray);
                });

                // Spacer between signature and details
                row.ConstantItem(30);

                // --- Right: Certificate Details ---
                row.RelativeItem().Column(detCol =>
                {
                    detCol.Spacing(8);

                    // Certificate Number
                    detCol.Item().Row(r =>
                    {
                        r.ConstantItem(110).Text("Certificate No.").FontSize(7.5f).FontColor(LightGray);
                        r.RelativeItem().Text(certificate.CertificateNumber).FontSize(7).FontColor(DarkText);
                    });

                    // Verification Code
                    detCol.Item().Row(r =>
                    {
                        r.ConstantItem(110).Text("Verification Code").FontSize(7.5f).FontColor(LightGray);
                        r.RelativeItem().Text(certificate.VerificationCode).FontSize(7).FontColor(DarkText);
                    });

                    // Issue Date
                    detCol.Item().Row(r =>
                    {
                        r.ConstantItem(110).Text("Issue Date").FontSize(7.5f).FontColor(LightGray);
                        r.RelativeItem().Text(certificate.IssuedAt.ToString("yyyy-MM-dd")).FontSize(7).FontColor(DarkText);
                    });
                });
            });
        }

        // =====================================================================
        // UTILITY
        // =====================================================================
        /// <summary>
        /// Calculates adaptive font size based on text length
        /// </summary>
        private float GetAdaptiveFontSize(string text, float maxSize, float minSize)
        {
            if (string.IsNullOrWhiteSpace(text))
                return maxSize;

            int length = text.Length;

            if (length <= 20) return maxSize;
            if (length <= 30) return maxSize - 3;
            if (length <= 40) return maxSize - 5;
            if (length <= 50) return maxSize - 7;

            return Math.Max(minSize, maxSize - 10);
        }
    }
}