namespace Luma.SmartHub
{
    /// <summary>
    /// Declares a service which initializes the modules into the application.
    /// </summary>
    public interface IModuleInitializer
    {
        /// <summary>
        /// Initializes the specified module.
        /// </summary>
        void Initialize();
    }
}
