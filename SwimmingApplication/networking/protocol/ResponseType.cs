﻿namespace networking.protocol;

public enum ResponseType
{
    OK, 
    ERROR,
    LOGIN_SUCCESS,
    LOGOUT_SUCCESS,
    REGISTER_SUCCESS,
    REGISTRATION_ADDED, 
    REGISTRATIONS_RETRIEVED, 
    RACES_RETRIEVED, 
    RACE_RETRIEVED, 
    PARTICIPANTS_RETRIEVED, 
    PARTICIPANT_RETRIEVED,
    USERS_RETRIEVED,
    UPDATE_REGISTRATION
}