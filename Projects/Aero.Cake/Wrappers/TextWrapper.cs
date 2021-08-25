using Cake.Core.IO;
using Cake.FileHelpers;

namespace Aero.Cake.Wrappers
{
    public interface ITextWrapper
    {
        FilePath[] ReplaceTextInFile(IFile file, string findText, string replaceText);
        FilePath[] ReplaceTextInFiles(string globberPattern, string findText, string replaceText);
    }

    public class TextWrapper : AbstractWrapper, ITextWrapper
    {
        public TextWrapper(IAeroContext aeroContext) : base(aeroContext)
        {

        }

        public FilePath[] ReplaceTextInFile(IFile file, string findText, string replaceText)
        {
            return AeroContext.ReplaceTextInFiles(file.Path.FullPath, findText, replaceText);
        }

        public FilePath[] ReplaceTextInFiles(string globberPattern, string findText, string replaceText)
        {
            return AeroContext.ReplaceTextInFiles(globberPattern, findText, replaceText);
        }
    }
}