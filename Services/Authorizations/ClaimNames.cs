using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public static class ClaimNames
    {
        public const string RoleRead = "role:read";
        public const string RoleWrite = "role:write";
        public const string ClaimRead = "claim:read";
        public const string ClaimWrite = "claim:write";
        public const string RoleClaimRead = "roleclaim:read";
        public const string RoleClaimWrite = "roleclaim:write";
        public const string UserRead = "user:read";
        public const string UserWrite = "user:write";
        public const string PlaceRead = "place:read";
        public const string PlaceWrite = "place:write";
        public const string EventRead = "event:read";
        public const string EventWrite = "event:write";
        public const string EventAttendeeRead = "eventattendee:read";
        public const string EventAttendeeWrite = "eventattendee:write";
        public const string EventCommentRead = "eventcomment:read";
        public const string EventCommentWrite = "eventcomment:write";
        public const string EventChatRead = "eventchat:read";
        public const string EventChatWrite = "eventchat:write";
    }
}
