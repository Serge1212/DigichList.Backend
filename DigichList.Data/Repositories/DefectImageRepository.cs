using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class DefectImageRepository : IDefectImageRepository
    {

        public async Task<DefectImage> SaveImageAsStringByteArray(string path)
        {
            var imageArray = await File.ReadAllBytesAsync(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            using DigichListContext context = new DigichListContext();
            DefectImage defectImage = new DefectImage
            {
                Image = base64ImageRepresentation
            };
            return defectImage;
        }
    }
}
