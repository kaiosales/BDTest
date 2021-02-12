using Microsoft.EntityFrameworkCore.Storage;

namespace BDTest.Data
{
    public static class DatabaseRoot
    {
        public static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();
    }
}