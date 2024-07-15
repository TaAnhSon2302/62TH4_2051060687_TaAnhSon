using UnityEditor;
using UnityEngine;
public static class GameStatic
{
    public static bool IS_ANIMATING = false;
    #region game property
    public readonly static int ARMOR_COEFFICIENT = 300;
    public readonly static float GUN_MAX_SPREAD_ANGLE = 60;
    public readonly static float STATUS_DURATION = 10f;
    public readonly static float TIME_BETWEEN_STATUS_TICK = 1f;
    public readonly static int MAX_STATUS_STACK = 10;
    public readonly static int UNLIMITED_STATUS_STACK = int.MaxValue;
    #endregion

    #region color
    public readonly static Color CRITICAL_TIER_0_COLOR = Color.white;
    public readonly static Color CRITICAL_TIER_1_COLOR = Color.yellow;
    public readonly static Color CRITICAL_TIER_2_COLOR = new Color(1f, 0.5f, 0f, 1f);
    public readonly static Color CRITICAL_TIER_3_COLOR = Color.red;
    public readonly static Color CRITICAL_TIER_4_COLOR = Color.red;
    public readonly static Color CRITICAL_TIER_5_COLOR = Color.red;
    public readonly static Color SHIELD_DAMAGE_COLOR = new Color(0f, 200f / 255f, 1f, 1f);
    public readonly static Color USER_UI_COLOR_BLUE = Color.blue;
    public readonly static Color USER_UI_COLOR_CYAN = Color.cyan;
    public readonly static Color USER_UI_COLOR_PURPLE = new Color(0.627451f, 0.1254902f, 0.9411765f, 1f);
    #endregion

    #region API
    //HOST
    public readonly static string HOST = "https://localhost:7121";
    //GET
    public readonly static string GET_MUTATION_API = "/api/Mutations";
    public readonly static string GET_ENEMY_API = "/api/EnemyCells";
    public readonly static string GET_ABILITY_API = "/api/MutationAbilities";
    public readonly static string GET_BULLET_API = "/api/Bullets";
    public readonly static string GET_INGAME_LEVEL_CONFIGS = "/api/IngameLevelConfigs";
    public readonly static string GET_USER_INFORMATION = "/api/Users/";
    public readonly static string GET_USER_GUN = "/api/UserGun/getUserGunList/";
    public readonly static string GET_GUN_API = "/api/Guns";
    public readonly static string GET_USER_EQUIPED_GUN = "/api/UserEquipment/GetUserEquipmentById/{id}?userid=";
    public readonly static string GET_USER_MUTATION = "/api/UserMutation/getUserMutationList/";

    //POST
    public readonly static string POST_LOGIN_REQUEST = "/identity/login";
    public readonly static string POST_SIGNUP_REQUEST = "/identity/register";
    #endregion
}