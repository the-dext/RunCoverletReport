namespace RunCoverletReport.Options
{
    using System.ComponentModel;

    public enum IntegrationType
    {
        [Description("Coverlet.Collector")]
        Collector,

        [Description("Coverlet.MSBuild")]
        MSBuild,
    }
}