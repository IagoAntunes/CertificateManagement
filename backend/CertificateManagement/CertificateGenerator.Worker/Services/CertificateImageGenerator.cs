using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;

namespace CertificateGenerator.Worker.Services
{
    public class CertificateImageGenerator
    {
        public async Task<Stream> GenerateImageAsync(
            string studentName,
            string activities,
            DateTime generationDate
            )
        {
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add("Assets/DancingScriptVariableFont.ttf");
            var interFontFamily = fontCollection.Add("Assets/InterVariableFont.ttf");

            using (var image = await Image.LoadAsync("Assets/certificate_template.png"))
            {
                // StudentName
                var centerX = image.Width / 2;
                var studentNameFont = fontFamily.CreateFont(50, FontStyle.Bold);
                var studentNameOptions = new RichTextOptions(studentNameFont)
                {
                    Origin = new PointF(centerX, 210), 
                    HorizontalAlignment = HorizontalAlignment.Center,
                    WrappingLength = image.Width - 200
                };

                // Activities
                var activitiesFont = interFontFamily.CreateFont(14, FontStyle.Regular);
                var activitiesOptions = new RichTextOptions(activitiesFont)
                {
                    Origin = new PointF(centerX, 340),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    WrappingLength = image.Width - 300
                };

                // Date
                var dateFont = interFontFamily.CreateFont(12, FontStyle.Bold);
                var dateOptions = new RichTextOptions(dateFont)
                {
                    Origin = new PointF(centerX, 450), 
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var formattedDate = generationDate.ToString("dd 'de' MMMM 'de' yyyy");

                image.Mutate(ctx =>
                {
                    ctx.DrawText(studentNameOptions, studentName, Color.FromRgb(59,90,194)); 
                    ctx.DrawText(activitiesOptions, activities, Color.FromRgb(18,22,33));
                    ctx.DrawText(dateOptions, formattedDate, Color.FromRgb(18,22,33));
                });

                
                var outputStream = new MemoryStream();
                await image.SaveAsPngAsync(outputStream);
                outputStream.Position = 0; 

                return outputStream;
            }
        }
    }
}
