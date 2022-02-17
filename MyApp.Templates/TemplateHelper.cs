using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyApp.Templates
{
	public static class TemplateHelper
    {
		private static readonly Assembly _resourceAssembly = typeof(TemplateHelper).Assembly;
		public static Stream GetImageStream(string imageName, out string filetype)
		{
			var resourceName = _resourceAssembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(imageName, StringComparison.InvariantCultureIgnoreCase));
			if (!string.IsNullOrEmpty(resourceName))
			{
				filetype = Path.GetExtension(imageName).ToLowerInvariant() switch
				{
					".jpg" => "image/jpeg",
					".png" => "image/png",
					_ => null,
				};
				if (!string.IsNullOrEmpty(filetype))
				{
					return _resourceAssembly.GetManifestResourceStream(resourceName);
				}
			}
			filetype = null;
			return null;
		}

		public static string BuildImageSource(string imageName)
		{
			using var stream = GetImageStream(imageName, out var fileType);
			if (stream is null) return "";
			using var memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			var base64Data = Convert.ToBase64String(memoryStream.GetBuffer().AsSpan()[..(int)memoryStream.Length]);
			return $"data:{fileType};base64, {base64Data}";
		}

		public static string[] GetLogos()
			=> _resourceAssembly.GetManifestResourceNames().Select(s => s.Replace("MyApp.Templates.Logo.","")).ToArray();
	}
}
