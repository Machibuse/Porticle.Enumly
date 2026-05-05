; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
EM0001  | Enumly   | Error    | Source enum value has no matching target member
EM0002  | Enumly   | Error    | Invalid enum mapping method signature
EM0003  | Enumly   | Error    | NullSourceValue requires a nullable target
EM0004  | Enumly   | Error    | Specified null value is not a member of its enum
EM0005  | Enumly   | Error    | Null value has wrong enum type
EM0006  | Enumly   | Error    | EnumlyMapValue argument has wrong enum type
EM0007  | Enumly   | Error    | Duplicate explicit mapping for source value
EM0008  | Enumly   | Warning  | Target enum value is not reachable from any source
EM0009  | Enumly   | Error    | EnumlyIgnoreSource/EnumlyIgnoreTarget argument has wrong enum type
EM0010  | Enumly   | Error    | Nullable source requires NullTargetValue when target is non-nullable
