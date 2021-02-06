using CSharpFunctionalExtensions;
using Shop.Shared.Shared;
using System;
using System.Collections.Generic;

namespace Shop.Store.Core.BookContent
{
    public class FileContent : ValueObject
    {
        public byte[] File { get; }
        public string FileTitle { get; }
        public static FileContent Default => new(Array.Empty<byte>(), string.Empty);
        private FileContent(byte[] file, string fileTitle)
        {
            File = file;
            FileTitle = fileTitle;
        }
        public static Result<FileContent> Create(byte[] file, string fileTitle) =>
            file is null || file.Length == 0 ? Result.Failure<FileContent>($"{nameof(file)} can't be null or empty") :
            fileTitle.IsEmpty() ? Result.Failure<FileContent>($"{nameof(fileTitle)} can't be null or empty") :
            Result.Success(new FileContent(file, fileTitle));
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return File;
            yield return FileTitle;
        }
    }
}
