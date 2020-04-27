using Cake.Core;
using System;

namespace Aero.Cake
{
    /// <summary>
    /// An Aero cake MyContext for the test project
    /// </summary>
    public class MyContext : AeroContext
    {
        public MyContext(ICakeContext cakeContext) : base(cakeContext)
        {

        }

        public override string GetNormalizedPath(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}
