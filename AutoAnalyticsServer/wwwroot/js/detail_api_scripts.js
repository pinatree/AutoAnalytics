//fill subgroups select-options from API, with our group
function reloadGroups() {

    //get json data from api
    $.getJSON("/api/group", function (json) {

        //clear our subgroups
        $(group_lb).empty();

        //fill groups in cycle
        $.each(json, function (index, value) {
            $(group_lb).append('<option>' + value + '</option>');
        });

        $(subgroup_lb).prop("disabled", false);
        reloadSubgroups($(group_lb).val());
    });
}

//fill subgroups select-options from API, with our group
function reloadSubgroups(search_group) {

    //get json data from api
    $.getJSON("/api/subgroup",
        {
            groupName: search_group
        },
        function (json) {

            //clear our subgroups
            $(subgroup_lb).empty();

            //fill subgroups in cycle
            $.each(json, function (index, value) {
                $(subgroup_lb).append('<option>' + value + '</option>');
            });

            $(detail_lb).prop("disabled", false);
            reloadDetails($(group_lb).val(), $(subgroup_lb).val());
        });
}

function reloadDetails(search_group, search_subgroup) {

    //get json data from api
    $.getJSON("/api/detail",
        {
            groupName: search_group,
            subgroupName: search_subgroup
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

function addRecommendDetails(search_group, search_subgroup, search_detail) {
    //get json data from api
    $.getJSON("/api/recommendation",
        {
            groupName: search_group,
            subgroupName: search_subgroup,
            detailName: search_detail
        },
        function (json) {

            $.each(json, function (index, value) {
                $('#recommendations_table > tbody:last-child').append('<tr><th>'
                    + value.group   
                    + '</th><td>'
                    + value.subgroup
                    + '</td><td>'
                    + value.detail
                    + '</td><td>'
                    + value.confidence
                    + '</td></tr>');
            });            
        });
}