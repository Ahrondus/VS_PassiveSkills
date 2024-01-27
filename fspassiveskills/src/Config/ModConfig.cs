using Vintagestory.API.Common;

namespace FSPassiveSkills.Config
{
  public static class ModConfig
  {
    private const string jsonConfig = "FSPassiveSkillsConfig.json";
    private static FSPassiveSkillsConfig config;

    public static void ReadConfig(ICoreAPI api)
    {
      try
      {
        config = LoadConfig(api);

        if (config == null)
        {
          api.World.Logger.Event("Creating New 'FS Passive Skills' Config");
          GenerateConfig(api);
          config = LoadConfig(api);
        }
        else
        {
			    api.World.Logger.Event("Reading 'FS Passive Skills' Config");
          GenerateConfig(api, config);
        }
      }
      catch
      {
        api.World.Logger.Event("Creating New 'FS Passive Skills' Config");
        GenerateConfig(api);
        config = LoadConfig(api);
      }
      
    }

    private static FSPassiveSkillsConfig LoadConfig(ICoreAPI api) =>
      api.LoadModConfig<FSPassiveSkillsConfig>(jsonConfig);

    private static void GenerateConfig(ICoreAPI api) =>
      api.StoreModConfig(new FSPassiveSkillsConfig(), jsonConfig);

    private static void GenerateConfig(ICoreAPI api, FSPassiveSkillsConfig previousConfig) =>
      api.StoreModConfig(new FSPassiveSkillsConfig(previousConfig), jsonConfig);
  }
}