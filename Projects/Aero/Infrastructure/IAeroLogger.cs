﻿using System;
using System.Runtime.CompilerServices;

namespace Aero.Infrastructure
{
    public interface IAeroLogger
    {
        void Debug(string message, Exception ex = null, [CallerMemberName] string memberName = "X");

        void Error(string message, Exception ex = null, [CallerMemberName] string memberName = "X");

        void Fatal(string message, Exception ex = null, [CallerMemberName] string memberName = "X");

        void Info(string message, Exception ex = null, [CallerMemberName] string memberName = "X");

        void Trace(string message, Exception ex = null, [CallerMemberName] string memberName = "X");

        void Warn(string message, Exception ex = null, [CallerMemberName] string memberName = "X");
    }

    public interface IAeroLogger<T> : IAeroLogger
    {

    }
}
