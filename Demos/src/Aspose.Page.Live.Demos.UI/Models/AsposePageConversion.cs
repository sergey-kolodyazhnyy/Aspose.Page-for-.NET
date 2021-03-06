using Aspose.Page.Live.Demos.UI.Models;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Aspose.Page.EPS;
using Aspose.Page.EPS.Device;
using Aspose.Page.XPS;
using Aspose.Page.XPS.Presentation.Image;

namespace Aspose.Page.Live.Demos.UI.Models
{
	///<Summary>
	/// AsposePageConversion class to convert page file to other format
	///</Summary>
	public class AsposePageConversion : PageBase
	{
		///<Summary>
		/// Convert method
		///</Summary>

		public Response Convert(AsposePageDocumentInfo[] docs, string outputType, string sourceFolder)
		{
			
			if (docs == null)
				return BadDocumentResponse;
			if (docs.Length == 0 || docs.Length > MaximumUploadFiles)
				return MaximumFileLimitsResponse;

			string inputType = docs[0].InputType;

			SetDefaultOptions(docs, outputType);
			Opts.AppName = "Conversion";
			Opts.MethodName = "Convert";
			Opts.ZipFileName = docs.Length > 1 ? "Converted documents" : Path.GetFileNameWithoutExtension(docs[0].FileName);

			var saveOpt = GetSaveOptions(outputType.ToLower(), inputType);

			return  Process((inFilePath, outPath, zipOutFolder) =>
			{
				var tasks = docs.Select(doc => Task.Factory.StartNew(() => SaveDocument(doc, outPath, zipOutFolder, saveOpt))).ToArray();
				Task.WaitAll(tasks);
			});
		}
		
	}
}
