﻿namespace Carlton.Core.Lab.Models.Common;
public record ComponentState
{
    [Required]
    public required string DisplayName { get; init; }
    [Required]
    public required object ComponentParameters { get; init; }
};

