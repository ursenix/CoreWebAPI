using System;
using Microsoft.Azure.Documents; 
using Microsoft.Azure.Documents.Client; 
using Microsoft.Azure.Documents.Linq; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.WebUtilities;

public class DocumentDBRepository
 {
    private readonly DocumentDBSetting _dbSettings;
    private readonly string DatabaseId;
    private readonly string CollectionId;
    private readonly string EndPoint;
    private readonly string AuthKey;

    private DocumentClient client;

     public DocumentDBRepository(DocumentDBSetting dbSettings)
     {
        this._dbSettings = dbSettings;

        this.DatabaseId = _dbSettings.Database;
        this.CollectionId = _dbSettings.Collection;
        this.EndPoint = _dbSettings.Endpoint;
        this.AuthKey = _dbSettings.AuthKey;
     }

     public void Initialize()
     {
         client = new DocumentClient(new Uri(EndPoint), AuthKey);
         CreateDatabaseIfNotExistsAsync().Wait();
         CreateCollectionIfNotExistsAsync().Wait();
     }

     private async Task CreateDatabaseIfNotExistsAsync()
     {
         try
         {
             await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
         }
         catch (DocumentClientException e)
         {
             if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
             {
                 await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
             }
             else
             {
                 throw;
             }
         }
     }

     private async Task CreateCollectionIfNotExistsAsync()
     {
         try
         {
             await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
         }
         catch (DocumentClientException e)
         {
             if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
             {
                 await client.CreateDocumentCollectionAsync(
                     UriFactory.CreateDatabaseUri(DatabaseId),
                     new DocumentCollection { Id = CollectionId },
                     new RequestOptions { OfferThroughput = 1000 });
             }
             else
             {
                 throw;
             }
         }
     }
 }