using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBotTest
{
    public class ResponseRoot
    {
        public bool ok { get; set; }
        public Self self { get; set; }
        public Team team { get; set; }
        public string latest_event_ts { get; set; }
        public Channel[] channels { get; set; }
        public Group[] groups { get; set; }
        public Im[] ims { get; set; }
        public int cache_ts { get; set; }
        public object[] read_only_channels { get; set; }
        public bool can_manage_shared_channels { get; set; }
        public Subteams subteams { get; set; }
        public Dnd dnd { get; set; }
        public User[] users { get; set; }
        public string cache_version { get; set; }
        public string cache_ts_version { get; set; }
        public Bot[] bots { get; set; }
        public string url { get; set; }
    }

    public class Self
    {
        public string id { get; set; }
        public string name { get; set; }
        public Prefs prefs { get; set; }
        public int created { get; set; }
        public string manual_presence { get; set; }
    }

    public class Prefs
    {
        public string highlight_words { get; set; }
        public string user_colors { get; set; }
        public bool color_names_in_list { get; set; }
        public bool growls_enabled { get; set; }
        public object tz { get; set; }
        public bool push_dm_alert { get; set; }
        public bool push_mention_alert { get; set; }
        public bool push_everything { get; set; }
        public bool push_show_preview { get; set; }
        public int push_idle_wait { get; set; }
        public string push_sound { get; set; }
        public string push_loud_channels { get; set; }
        public string push_mention_channels { get; set; }
        public string push_loud_channels_set { get; set; }
        public bool threads_everything { get; set; }
        public string email_alerts { get; set; }
        public int email_alerts_sleep_until { get; set; }
        public bool email_misc { get; set; }
        public bool email_weekly { get; set; }
        public bool welcome_message_hidden { get; set; }
        public bool all_channels_loud { get; set; }
        public string loud_channels { get; set; }
        public string never_channels { get; set; }
        public string loud_channels_set { get; set; }
        public string search_sort { get; set; }
        public bool expand_inline_imgs { get; set; }
        public bool expand_internal_inline_imgs { get; set; }
        public bool expand_snippets { get; set; }
        public bool posts_formatting_guide { get; set; }
        public bool seen_welcome_2 { get; set; }
        public bool seen_ssb_prompt { get; set; }
        public bool spaces_new_xp_banner_dismissed { get; set; }
        public bool search_only_my_channels { get; set; }
        public bool search_only_current_team { get; set; }
        public string emoji_mode { get; set; }
        public string emoji_use { get; set; }
        public bool has_invited { get; set; }
        public bool has_uploaded { get; set; }
        public bool has_created_channel { get; set; }
        public bool has_searched { get; set; }
        public string search_exclude_channels { get; set; }
        public string messages_theme { get; set; }
        public bool webapp_spellcheck { get; set; }
        public bool no_joined_overlays { get; set; }
        public bool no_created_overlays { get; set; }
        public bool dropbox_enabled { get; set; }
        public bool seen_domain_invite_reminder { get; set; }
        public bool seen_member_invite_reminder { get; set; }
        public bool mute_sounds { get; set; }
        public bool arrow_history { get; set; }
        public bool tab_ui_return_selects { get; set; }
        public bool obey_inline_img_limit { get; set; }
        public string new_msg_snd { get; set; }
        public bool require_at { get; set; }
        public string ssb_space_window { get; set; }
        public string mac_ssb_bounce { get; set; }
        public bool mac_ssb_bullet { get; set; }
        public bool expand_non_media_attachments { get; set; }
        public bool show_typing { get; set; }
        public bool pagekeys_handled { get; set; }
        public string last_snippet_type { get; set; }
        public int display_real_names_override { get; set; }
        public bool display_preferred_names { get; set; }
        public bool time24 { get; set; }
        public bool enter_is_special_in_tbt { get; set; }
        public bool graphic_emoticons { get; set; }
        public bool convert_emoticons { get; set; }
        public bool ss_emojis { get; set; }
        public string sidebar_behavior { get; set; }
        public bool seen_onboarding_start { get; set; }
        public bool onboarding_cancelled { get; set; }
        public bool seen_onboarding_slackbot_conversation { get; set; }
        public bool seen_onboarding_channels { get; set; }
        public bool seen_onboarding_direct_messages { get; set; }
        public bool seen_onboarding_invites { get; set; }
        public bool seen_onboarding_search { get; set; }
        public bool seen_onboarding_recent_mentions { get; set; }
        public bool seen_onboarding_starred_items { get; set; }
        public bool seen_onboarding_private_groups { get; set; }
        public int onboarding_slackbot_conversation_step { get; set; }
        public bool dnd_enabled { get; set; }
        public string dnd_start_hour { get; set; }
        public string dnd_end_hour { get; set; }
        public bool mark_msgs_read_immediately { get; set; }
        public bool start_scroll_at_oldest { get; set; }
        public bool snippet_editor_wrap_long_lines { get; set; }
        public bool ls_disabled { get; set; }
        public string sidebar_theme { get; set; }
        public string sidebar_theme_custom_values { get; set; }
        public bool f_key_search { get; set; }
        public bool k_key_omnibox { get; set; }
        public string at_channel_suppressed_channels { get; set; }
        public string push_at_channel_suppressed_channels { get; set; }
        public bool prompted_for_email_disabling { get; set; }
        public bool full_text_extracts { get; set; }
        public bool no_text_in_notifications { get; set; }
        public string muted_channels { get; set; }
        public bool no_macelectron_banner { get; set; }
        public bool no_macssb1_banner { get; set; }
        public bool no_macssb2_banner { get; set; }
        public bool no_winssb1_banner { get; set; }
        public bool no_invites_widget_in_sidebar { get; set; }
        public bool no_omnibox_in_channels { get; set; }
        public int k_key_omnibox_auto_hide_count { get; set; }
        public bool prev_next_btn { get; set; }
        public bool hide_user_group_info_pane { get; set; }
        public bool mentions_exclude_at_user_groups { get; set; }
        public bool privacy_policy_seen { get; set; }
        public bool enterprise_migration_seen { get; set; }
        public object last_tos_acknowledged { get; set; }
        public bool search_exclude_bots { get; set; }
        public bool load_lato_2 { get; set; }
        public bool fuller_timestamps { get; set; }
        public int last_seen_at_channel_warning { get; set; }
        public bool msg_preview { get; set; }
        public bool msg_preview_persistent { get; set; }
        public bool emoji_autocomplete_big { get; set; }
        public bool winssb_run_from_tray { get; set; }
        public string winssb_window_flash_behavior { get; set; }
        public bool two_factor_auth_enabled { get; set; }
        public object two_factor_type { get; set; }
        public object two_factor_backup_type { get; set; }
        public bool hide_hex_swatch { get; set; }
        public bool show_jumper_scores { get; set; }
        public string client_logs_pri { get; set; }
        public bool enhanced_debugging { get; set; }
        public string flannel_server_pool { get; set; }
        public bool mentions_exclude_at_channels { get; set; }
        public bool confirm_clear_all_unreads { get; set; }
        public bool confirm_user_marked_away { get; set; }
        public bool box_enabled { get; set; }
        public bool seen_single_emoji_msg { get; set; }
        public bool confirm_sh_call_start { get; set; }
        public string preferred_skin_tone { get; set; }
        public bool show_all_skin_tones { get; set; }
        public bool separate_private_channels { get; set; }
        public int whats_new_read { get; set; }
        public string frecency_jumper { get; set; }
        public string frecency_ent_jumper { get; set; }
        public bool jumbomoji { get; set; }
        public int newxp_seen_last_message { get; set; }
        public bool attachments_with_borders { get; set; }
        public bool show_memory_instrument { get; set; }
        public bool enable_unread_view { get; set; }
        public bool seen_unread_view_coachmark { get; set; }
        public bool seen_calls_video_beta_coachmark { get; set; }
        public bool seen_calls_video_ga_coachmark { get; set; }
        public bool seen_calls_ss_window_coachmark { get; set; }
        public bool measure_css_usage { get; set; }
        public bool enable_react_emoji_picker { get; set; }
        public bool seen_replies_coachmark { get; set; }
        public bool seen_custom_status_badge { get; set; }
        public bool seen_threads_notification_banner { get; set; }
        public bool all_unreads_sort_order { get; set; }
        public string locale { get; set; }
        public bool seen_intl_channel_names_coachmark { get; set; }
        public bool gdrive_authed { get; set; }
        public bool gdrive_enabled { get; set; }
        public bool seen_gdrive_coachmark { get; set; }
        public string channel_sort { get; set; }
        public bool overloaded_message_enabled { get; set; }
        public bool seen_highlights_coachmark { get; set; }
        public bool seen_highlights_arrows_coachmark { get; set; }
        public string a11y_font_size { get; set; }
        public bool a11y_animations { get; set; }
        public bool intro_to_apps_message_seen { get; set; }
    }

    public class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email_domain { get; set; }
        public string domain { get; set; }
        public int msg_edit_window_mins { get; set; }
        public Prefs1 prefs { get; set; }
        public Icon icon { get; set; }
        public bool over_storage_limit { get; set; }
        public string plan { get; set; }
        public string avatar_base_url { get; set; }
        public bool over_integrations_limit { get; set; }
    }

    public class Prefs1
    {
        public string[] default_channels { get; set; }
        public string who_can_create_channels { get; set; }
        public string who_can_archive_channels { get; set; }
        public string who_can_create_groups { get; set; }
        public string who_can_kick_channels { get; set; }
        public string who_can_kick_groups { get; set; }
        public bool display_real_names { get; set; }
        public bool slackbot_responses_disabled { get; set; }
        public bool slackbot_responses_only_admins { get; set; }
        public bool emoji_only_admins { get; set; }
        public bool loading_only_admins { get; set; }
        public int posts_migrating { get; set; }
        public bool gateway_allow_xmpp_ssl { get; set; }
        public bool gateway_allow_irc_ssl { get; set; }
        public bool gateway_allow_irc_plain { get; set; }
        public bool allow_calls { get; set; }
        public bool display_email_addresses { get; set; }
        public bool gdrive_enabled_team { get; set; }
        public string locale { get; set; }
        public bool hide_referers { get; set; }
        public int msg_edit_window_mins { get; set; }
        public bool allow_message_deletion { get; set; }
        public string calling_app_name { get; set; }
        public string who_can_at_everyone { get; set; }
        public string who_can_at_channel { get; set; }
        public string who_can_post_general { get; set; }
        public int retention_type { get; set; }
        public int retention_duration { get; set; }
        public int group_retention_type { get; set; }
        public int group_retention_duration { get; set; }
        public int dm_retention_type { get; set; }
        public int dm_retention_duration { get; set; }
        public int file_retention_duration { get; set; }
        public int file_retention_type { get; set; }
        public bool allow_retention_override { get; set; }
        public bool require_at_for_mention { get; set; }
        public string[] default_rxns { get; set; }
        public Team_Handy_Rxns team_handy_rxns { get; set; }
        public object channel_handy_rxns { get; set; }
        public int compliance_export_start { get; set; }
        public string warn_before_at_channel { get; set; }
        public bool disallow_public_file_urls { get; set; }
        public string who_can_create_delete_user_groups { get; set; }
        public string who_can_edit_user_groups { get; set; }
        public string who_can_change_team_profile { get; set; }
        public bool allow_shared_channels { get; set; }
        public string who_has_team_visibility { get; set; }
        public bool invites_only_admins { get; set; }
        public string disable_file_uploads { get; set; }
        public bool disable_file_editing { get; set; }
        public bool disable_file_deleting { get; set; }
        public string who_can_create_shared_channels { get; set; }
        public Who_Can_Manage_Shared_Channels who_can_manage_shared_channels { get; set; }
        public Who_Can_Post_In_Shared_Channels who_can_post_in_shared_channels { get; set; }
        public bool allow_shared_channel_perms_override { get; set; }
        public Enterprise_Team_Creation_Request enterprise_team_creation_request { get; set; }
        public object[] enterprise_default_channels { get; set; }
        public object[] enterprise_mandatory_channels { get; set; }
        public bool dnd_enabled { get; set; }
        public string dnd_start_hour { get; set; }
        public string dnd_end_hour { get; set; }
        public string auth_mode { get; set; }
        public Who_Can_Manage_Integrations who_can_manage_integrations { get; set; }
        public string discoverable { get; set; }
        public bool invites_limit { get; set; }
    }

    public class Team_Handy_Rxns
    {
        public bool restrict { get; set; }
        public List[] list { get; set; }
    }

    public class List
    {
        public string name { get; set; }
        public string title { get; set; }
    }

    public class Who_Can_Manage_Shared_Channels
    {
        public string[] type { get; set; }
    }

    public class Who_Can_Post_In_Shared_Channels
    {
        public string[] type { get; set; }
    }

    public class Enterprise_Team_Creation_Request
    {
        public bool is_enabled { get; set; }
    }

    public class Who_Can_Manage_Integrations
    {
        public string[] type { get; set; }
    }

    public class Icon
    {
        public string image_34 { get; set; }
        public string image_44 { get; set; }
        public string image_68 { get; set; }
        public string image_88 { get; set; }
        public string image_102 { get; set; }
        public string image_132 { get; set; }
        public string image_original { get; set; }
        public string image_230 { get; set; }
    }

    public class Subteams
    {
        public object[] self { get; set; }
        public object[] all { get; set; }
    }

    public class Dnd
    {
        public bool dnd_enabled { get; set; }
        public int next_dnd_start_ts { get; set; }
        public int next_dnd_end_ts { get; set; }
        public bool snooze_enabled { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool is_channel { get; set; }
        public int created { get; set; }
        public string creator { get; set; }
        public bool is_archived { get; set; }
        public bool is_general { get; set; }
        public string name_normalized { get; set; }
        public bool has_pins { get; set; }
        public bool is_member { get; set; }
        public string[] previous_names { get; set; }
        public string last_read { get; set; }
        public Latest latest { get; set; }
        public int unread_count { get; set; }
        public int unread_count_display { get; set; }
        public string[] members { get; set; }
        public Topic topic { get; set; }
        public Purpose purpose { get; set; }
    }

    public class Latest
    {
        public string type { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string ts { get; set; }
    }

    public class Topic
    {
        public string value { get; set; }
        public string creator { get; set; }
        public int last_set { get; set; }
    }

    public class Purpose
    {
        public string value { get; set; }
        public string creator { get; set; }
        public int last_set { get; set; }
    }

    public class Im
    {
        public string id { get; set; }
        public int created { get; set; }
        public bool is_im { get; set; }
        public bool is_org_shared { get; set; }
        public string user { get; set; }
        public bool has_pins { get; set; }
        public string last_read { get; set; }
        public object latest { get; set; }
        public int unread_count { get; set; }
        public int unread_count_display { get; set; }
        public bool is_open { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string team_id { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
        public object status { get; set; }
        public string color { get; set; }
        public string real_name { get; set; }
        public string tz { get; set; }
        public string tz_label { get; set; }
        public int tz_offset { get; set; }
        public UserProfile profile { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_primary_owner { get; set; }
        public bool is_restricted { get; set; }
        public bool is_ultra_restricted { get; set; }
        public bool is_bot { get; set; }
        public int updated { get; set; }
        public string presence { get; set; }
    }

    public class UserProfile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar_hash { get; set; }
        public object fields { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string skype { get; set; }
        public string image_24 { get; set; }
        public string image_32 { get; set; }
        public string image_48 { get; set; }
        public string image_72 { get; set; }
        public string image_192 { get; set; }
        public string image_512 { get; set; }
        public string image_1024 { get; set; }
        public string image_original { get; set; }
        public string real_name { get; set; }
        public string real_name_normalized { get; set; }
        public string email { get; set; }
        public string bot_id { get; set; }
        public string api_app_id { get; set; }
        public bool always_active { get; set; }
    }

    public class Bot
    {
        public string id { get; set; }
        public bool deleted { get; set; }
        public string name { get; set; }
        public int updated { get; set; }
        public string app_id { get; set; }
        public Icons icons { get; set; }
    }

    public class Icons
    {
        public string image_36 { get; set; }
        public string image_48 { get; set; }
        public string image_72 { get; set; }
    }

    public class Group
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool is_group { get; set; }
        public int created { get; set; }
        public string creator { get; set; }
        public bool is_archived { get; set; }
        public string name_normalized { get; set; }
        public bool is_mpim { get; set; }
        public bool has_pins { get; set; }
        public bool is_open { get; set; }
        public string last_read { get; set; }
        public Latest latest { get; set; }
        public int unread_count { get; set; }
        public int unread_count_display { get; set; }
        public string[] members { get; set; }
        public Topic topic { get; set; }
        public Purpose purpose { get; set; }
    }

    public class Presence
    {
        public string type { get; set; }
        public string presence { get; set; }
        public string user { get; set; }
    }

}
