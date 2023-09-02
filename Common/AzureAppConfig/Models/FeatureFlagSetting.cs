namespace Common.AzureAppConfig.Models;

public class FeatureFlagSetting
{
    public string KeyName { get; }
    public bool Value { get; }

    public FeatureFlagSetting(string keyName, bool value)
    {
        KeyName = keyName;
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj is not FeatureFlagSetting setting)
        {
            return false;
        }

        return KeyName == setting.KeyName && Value == setting.Value;
    }

    public override int GetHashCode()
    {
        return KeyName.GetHashCode() * Value.GetHashCode();
    }
}