﻿namespace Tycoon.Factory.Core.Model;
public record ActivityDefinition(int Id, string Name, bool MultipleWorkers, TimeSpan RestPeriod);
