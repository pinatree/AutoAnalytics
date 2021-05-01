//fill subgroups select-options from API, with our group
function reloadGroups() {

    //get json data from api
    $.getJSON("/api/group", function (json) {

        //clear our subgroups
        $(group_lb).empty();

        //fill subgroups in cycle
        $.each(json, function (index, value) {
            $(group_lb).append('<option>' + value + '</option>');
        });
    });
}

//fill subgroups select-options from API, with our group
function reloadSubgroups(search_group) {

    //get json data from api
    $.getJSON("/api/subgroup",
        {
            group: search_group
        },
        function (json) {

            //clear our subgroups
            $(subgroup_lb).empty();

            //fill subgroups in cycle
            $.each(json, function (index, value) {
                $(subgroup_lb).append('<option>' + value + '</option>');
            });
        });
}

function reloadDetails(search_group, search_subgroup) {

    //get json data from api
    $.getJSON("/api/detail",
        {
            group: search_group,
            subgroup: search_subgroup
        },
        function (json) {

            //clear our groups
            $(detail_lb).empty();

            //fill groups in cycle
            $.each(json, function (index, value) {
                $(detail_lb).append('<option>' + value + '</option>');
            });
        });
}

function getRecommendDetails(search_group, search_subgroup, search_detail) {
    //get json data from api
    $.getJSON("/api/recommendations",
        {
            group: search_group,
            subgroup: search_subgroup,
            detail: search_detail
        },
        function (json) {
            return json;
        });
}