using GraphQlClientGenerator;
using EnvironmentManager.Extensions;
using Poolz.Finance.CSharp.DaoSender.Generator;

var url = Env.GRAPHQL_URL.GetRequired();
var schema = await GraphQlGenerator.RetrieveSchema(HttpMethod.Post, url, new List<KeyValuePair<string, string>>
{
    new("Authorization", Env.GRAPHQL_AUTH_TOKEN.GetRequired())
});
var configuration = new GraphQlGeneratorConfiguration
{
    TargetNamespace = Env.TARGET_NAMESPACE.GetRequired(),
    IdTypeMapping = IdTypeMapping.String,
    ScalarFieldTypeMappingProvider = new ScalarFieldTypeMappingProvider()
};
var generator = new GraphQlGenerator(configuration);
var csharpCode = generator.GenerateFullClientCSharpFile(schema);
await File.WriteAllTextAsync(Env.GENERATED_FILE_PATH.GetRequired(), csharpCode);