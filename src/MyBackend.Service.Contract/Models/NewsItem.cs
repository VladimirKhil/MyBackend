﻿namespace MyBackend.Service.Contract.Models;

public sealed record NewsItem(DateTimeOffset DateTime, string Text);
