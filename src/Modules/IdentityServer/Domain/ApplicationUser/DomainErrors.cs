using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Domain.ApplicationUser;

/// <summary>
///     Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    #region Nested type: Authentication

    /// <summary>
    ///     Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => new(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");
    }

    #endregion

    #region Nested type: Category

    /// <summary>
    ///     Contains the category errors.
    /// </summary>
    public static class Category
    {
        public static Error NotFound =>
            new("Category.NotFound", "The category with the specified identifier was not found.");
    }

    #endregion

    #region Nested type: Driver

    /// <summary>
    ///     Contains the Driver errors.
    /// </summary>
    public static class Driver
    {
        public static Error NotFound =>
            new("Driver.NotFound", "The attendee with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("Driver.AlreadyProcessed", "The attendee has already been processed.");
    }

    #endregion

    #region Nested type: Email

    /// <summary>
    ///     Contains the email errors.
    /// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
    }

    #endregion

    #region Nested type: Event

    /// <summary>
    ///     Contains the event errors.
    /// </summary>
    public static class Event
    {
        public static Error AlreadyCancelled => new("Event.AlreadyCancelled", "The event has already been cancelled.");

        public static Error EventHasPassed => new(
            "Event.EventHasPassed",
            "The event has already passed and cannot be modified.");
    }

    #endregion

    #region Nested type: FirstName

    /// <summary>
    ///     Contains the first name errors.
    /// </summary>
    public static class FirstName
    {
        public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed =>
            new("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }

    #endregion

    #region Nested type: Friendship

    /// <summary>
    ///     Contains the friendship errors.
    /// </summary>
    public static class Friendship
    {
        public static Error UserNotFound => new(
            "Friendship.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => new(
            "Friendship.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error NotFriends => new(
            "Friendship.NotFriends",
            "The specified users are not friend.");
    }

    #endregion

    #region Nested type: FriendshipRequest

    /// <summary>
    ///     Contains the friendship request errors.
    /// </summary>
    public static class FriendshipRequest
    {
        public static Error NotFound => new(
            "FriendshipRequest.NotFound",
            "The friendship request with the specified identifier was not found.");

        public static Error UserNotFound => new(
            "FriendshipRequest.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => new(
            "FriendshipRequest.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error AlreadyAccepted => new(
            "FriendshipRequest.AlreadyAccepted",
            "The friendship request has already been accepted.");

        public static Error AlreadyRejected => new(
            "FriendshipRequest.AlreadyRejected",
            "The friendship request has already been rejected.");

        public static Error AlreadyFriends => new(
            "FriendshipRequest.AlreadyFriends",
            "The friendship request can not be sent because the users are already friends.");

        public static Error PendingFriendshipRequest => new(
            "FriendshipRequest.PendingFriendshipRequest",
            "The friendship request can not be sent because there is a pending friendship request.");
    }

    #endregion

    #region Nested type: General

    /// <summary>
    ///     Contains general errors.
    /// </summary>
    public static class General
    {
        public static Error UnProcessableRequest => new(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }

    #endregion

    #region Nested type: GroupEvent

    /// <summary>
    ///     Contains the group event errors.
    /// </summary>
    public static class GroupEvent
    {
        public static Error NotFound => new(
            "GroupEvent.NotFound",
            "The group event with the specified identifier was not found.");

        public static Error UserNotFound => new(
            "GroupEvent.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => new(
            "GroupEvent.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error InvitationAlreadySent => new(
            "GroupEvent.InvitationAlreadySent",
            "The invitation for this event has already been sent to this user.");

        public static Error NotFriends => new(
            "GroupEvent.NotFriends",
            "The specified users are not friend.");

        public static Error DateAndTimeIsInThePast => new(
            "GroupEvent.InThePast",
            "The event date and time cannot be in the past.");
    }

    #endregion

    #region Nested type: Invitation

    /// <summary>
    ///     Contains the invitation errors.
    /// </summary>
    public static class Invitation
    {
        public static Error NotFound => new(
            "Invitation.NotFound",
            "The invitation with the specified identifier was not found.");

        public static Error EventNotFound => new(
            "Invitation.EventNotFound",
            "The event with the specified identifier was not found.");

        public static Error UserNotFound => new(
            "Invitation.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => new(
            "Invitation.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error AlreadyAccepted =>
            new("Invitation.AlreadyAccepted", "The invitation has already been accepted.");

        public static Error AlreadyRejected =>
            new("Invitation.AlreadyRejected", "The invitation has already been rejected.");
    }

    #endregion

    #region Nested type: LastName

    /// <summary>
    ///     Contains the last name errors.
    /// </summary>
    public static class LastName
    {
        public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed =>
            new("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    #endregion

    #region Nested type: Name

    /// <summary>
    ///     Contains the name errors.
    /// </summary>
    public static class Name
    {
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
    }

    #endregion

    #region Nested type: Notification

    /// <summary>
    ///     Contains the notification errors.
    /// </summary>
    public static class Notification
    {
        public static Error AlreadySent => new("Notification.AlreadySent", "The notification has already been sent.");
    }

    #endregion

    #region Nested type: Password

    /// <summary>
    ///     Contains the password errors.
    /// </summary>
    public static class Password
    {
        public static Error NullOrEmpty => new("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new("Password.TooShort", "The password is too short.");

        public static Error MissingUppercaseLetter => new(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    #endregion

    #region Nested type: PersonalEvent

    /// <summary>
    ///     Contains the personal event errors.
    /// </summary>
    public static class PersonalEvent
    {
        public static Error NotFound => new(
            "GroupEvent.NotFound",
            "The group event with the specified identifier was not found.");

        public static Error UserNotFound => new(
            "GroupEvent.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error DateAndTimeIsInThePast => new(
            "GroupEvent.InThePast",
            "The event date and time cannot be in the past.");

        public static Error AlreadyProcessed =>
            new("PersonalEvent.AlreadyProcessed", "The event has already been processed.");
    }

    #endregion

    #region Nested type: User

    /// <summary>
    ///     Contains the user errors.
    /// </summary>
    public static class User
    {
        public static Error NotFound => new("User.NotFound", "The user with the specified identifier was not found.");

        public static Error InvalidPermissions => new(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation.");

        public static Error DuplicateEmail => new("User.DuplicateEmail", "The specified email is already in use.");

        public static Error CannotChangePassword => new(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.");
    }

    #endregion
}