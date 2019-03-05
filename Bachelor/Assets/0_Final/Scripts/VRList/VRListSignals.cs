using System.Collections.Generic;

public class ShowListSignal { }

public class SelectListElementSignal
{
    public string description;
}

public class FilterListSignal
{
    public List<FilterTag> filterTags;
}

public class ShowSearchSignal { }

public class SearchListSignal { }

public class HideSearchSignal { }

public class HideListSignal { }