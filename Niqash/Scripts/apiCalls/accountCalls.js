$(document).ready(function () {

    var accountId = $(".account").attr("data-account-id"),
        currentUserId = $("#userId").text(),
        followTxt = $("#followTxt").text(),
        unFollowTxt = $("#unFollowTxt").text(),
        followersTxt = $("#followersTxt").text(),
        followingTxt = $("#followingTxt").text(),
        nofollowersMsg = $("#nofollowersMsg").text(),
        nofollowingMsg = $("#nofollowingMsg").text(),
        postTxt = $("#postTxt").text(),
        commentTxt = $("#commentTxt").text(),
        likeTxt = $("#likeTxt").text(),
        loveTxt = $("#loveTxt").text();


    if (currentUserId !== accountId) {
        $("#postForm").remove();
    }

    function checkUserIsFollower(id) {
        $.ajax({
            url: "/api/following?accountId=" + id,
            method: "put",
            dataType: "json",
            error: function (er) {
                console.log(er);
            },
            success: function (checkResult) {
                if (checkResult.currentUserIsFollower === true) {
                    $(".followBtn").addClass("active");
                    $(".followBtn").text(unFollowTxt);
                    $(".followBtn").attr("data-follow-state", "follower");
                }
            }
        });
    }

    function getFollowersAndFollowingNumber(idToCheck) {
        $.ajax({
            url: "/api/following/" + idToCheck,
            method: "get",
            error: function (er) {
                console.log(er);
            },
            success: function (result) {
                $(".followersCount span").text(result.followersCount);
                $(".followingCount span").text(result.followingCount);
            }
        });
    }

    function activateLikesAndLoves(post) {
        post.likes.forEach(function (like) {

            if (like.userId === currentUserId) {
                $(".posts .post[data-post-id='" + like.postId + "']").find(".js-like")
                    .attr("data-like-id", like.id)
                    .addClass("activeLike")
                    .addClass("js-unLike")
                    .removeClass("js-like");
            }

        });
        post.loves.forEach(function (love) {

            if (love.userId === currentUserId) {
                $(".posts .post[data-post-id='" + love.postId + "']").find(".js-love")
                    .attr("data-love-id", love.id)
                    .addClass("activeLove")
                    .addClass("js-unLove")
                    .removeClass("js-love");
            }

        });
    }

    function removeZeroFromReactBox() {
        $(".posts .post .reactingNumber").each(function () {
            var reactingNumber = $(this);
            if (reactingNumber.text() === "0") {
                reactingNumber.css("color", "var(--postBg)");
            } else {
                reactingNumber.css("color", "#fff");
            }
        });
    }

    function addDirectionToPosts() {
        //$(".posts .post .postContent").each(function () {
        //    var postContent = $(this).text();
        //    if (postContent.charCodeAt(0) > 200) {
        //        $(this).css("direction", "rtl");
        //    } else {
        //        $(this).css("direction", "ltr");
        //    }
        //});
    }

    function removePostActionsBtns() {
        $('.posts .post').each(function () {
            var postUserId = $(this).attr("data-user-id");

            if (postUserId !== currentUserId) {
                $(this).find(".postActions").empty();
            }
        });
    }


    // get user data
    $.ajax({
        url: "/api/users/" + accountId,
        method: "get",
        error: function (e) {

        },
        success: function (account) {

            document.title = account.firstName + " " + account.lastName;

            if (account.aboutMe === null) {
                account.aboutMe = ".........";
            }

            $(".account").prepend(`
                                <div class='userData text-center'>
                                <img src='${account.profilePicSrc}' />
                                <h2 class='text-capitalize'>${account.firstName} ${account.lastName}</h2>
                                <div class="aboutMe">
                                    <p>
                                        ' ${account.aboutMe} '
                                    </p>
                                </div>

                                <div class="followSection">
                                    <button type="button" data-follow-state="notFollower" class="followBtn siteBtn">${followTxt}</button>
                                    <div class="overflow">
                                        <div class="followersCount">
                                            ${followersTxt} 
                                            <span></span>
                                        </div>

                                        <div class="followingCount pull-right">
                                            ${followingTxt} 
                                            <span></span>
                                        </div>
                                    </div>
                                </div>

                                <div class="userStatistics overflow">
                                    <div>
                                        <i class="glyphicon glyphicon-list-alt" title="${postTxt}"></i> ${account.posts.length}
                                    </div>

                                    <div>
                                        <i class="glyphicon glyphicon-comment" title="${commentTxt}"></i> ${account.comments.length}
                                    </div>

                                    <div>
                                        <i class="glyphicon glyphicon-thumbs-up" title="${likeTxt}"></i> ${account.likes.length}
                                    </div>
                                    <div>
                                        <i class="glyphicon glyphicon-heart" title="${loveTxt}"></i> ${account.loves.length}
                                    </div>
                                </div>
                            </div>`);

            checkUserIsFollower(accountId);
            getFollowersAndFollowingNumber(accountId);
            if (accountId === currentUserId) {
                $(".followBtn").remove();
            }

            // loop user posts
            if (account.posts.length === 0) {
                $(".noPostsMsg").fadeToggle();
            } else {
                account.posts.reverse(function (a, b) { return a.id - b.id });
                account.posts.forEach(function (post) {
                    // DO NOT FORGET TO CHANGE postData var in create post
                    //var postData = "<div class='post' data-post-id='" + post.id + "' data-user-id='" + post.userId + "'><div class='overflow'><div class='userImageContainer'><img src=' " + post.user.profilePicSrc + "' class='postUserImage img-circle' /></div><div class='postDataAndActions'><div class='overflow'><div class='postData'><h5><a href='/" + post.userId + "'> " + post.user.firstName + " " + post.user.lastName + "</a></h5> <h6>" + post.publishDate + "</h6></div><div class='postActions'><i class='glyphicon glyphicon-trash' id='removePost' data-post-id=" + post.id + "></i><i class='glyphicon glyphicon-edit' id='editPost' data-post-id=" + post.id + "></i></div></div></div></div><p class='postContent lead'>" + post.content + "</p> <div class='reactBox text-center'><div class='reactBtn js-like'><i class='glyphicon glyphicon-thumbs-up'></i><span class='reactingNumber'>" + post.likes.length + "</span></div><div class='reactBtn js-love'><i class='glyphicon glyphicon-heart'></i><span class='reactingNumber'>" + post.loves.length + "</span></div><div class='reactBtn js-comment' data-comments='false''><i class='glyphicon glyphicon-comment'></i><span class='reactingNumber'>" + post.comments.length + "</span></div></div><div class='comments'></div></div>";
                    var postData = `
                                <div class='post' data-post-id='${post.id}' data-user-id='${post.userId}'>
                                    <div class='overflow'>
                                        <div class='userImageContainer'>
                                            <img src='${account.profilePicSrc}' class='postUserImage img-circle' />
                                        </div>
                                        <div class='postDataAndActions'>
                                            <div class='overflow'>
                                                <div class='postData'>
                                                    <h5><a href='/users/account/${post.userId}'>${account.firstName} ${account.lastName}</a></h5>
                                                    <h6>${post.publishDate}</h6>
                                                </div>
                                                <div class='postActions'>
                                                    <i class='glyphicon glyphicon-trash' id='removePost' data-post-id='${post.id}'></i>
                                                    <i class='glyphicon glyphicon-edit' id='editPost' data-post-id='${post.id}'></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <p class='postContent lead'>${post.content}</p>
                                    <div class='reactBox text-center'>
                                        <div class='reactBtn js-like'>
                                            <i class='glyphicon glyphicon-thumbs-up'></i>
                                            <span class='reactingNumber'>${post.likes.length}</span>
                                        </div>
                                        <div class='reactBtn js-love'>
                                            <i class='glyphicon glyphicon-heart'></i>
                                            <span class='reactingNumber'>${post.loves.length}</span>
                                        </div>
                                        <div class='reactBtn js-comment' data-comments='false' '>
                                            <i class='glyphicon glyphicon-comment'></i>
                                            <span class='reactingNumber'>${post.comments.length}</span>
                                        </div>
                                    </div>
                                    <div class='comments'>
                                    </div>
                                </div>
                            `;

                    $(".posts").append(postData);
                    activateLikesAndLoves(post);
                    removePostActionsBtns();
                    removeZeroFromReactBox();
                    addDirectionToPosts();
                });
            }


        }
    });

    // follow Button
    $(document).on("click", ".followBtn", function () {

        var followBtn = $(this),
            followState = $(this).attr("data-follow-state");

        // create post request
        if (followState === "notFollower") {
            $.ajax({
                url: "/api/following?idOfAccount=" + accountId,
                type: "POST",
                dataType: "json",
                error: function (err) {
                    console.log(err);
                },
                success: function (follow) {
                    followBtn.addClass("active");
                    followBtn.text(unFollowTxt);
                    followBtn.attr("data-follow-state", "follower");
                }
            });
        }

        // create Delete request
        else {
            $.ajax({
                url: "/api/following?idForAccount=" + accountId,
                type: "DELETE",
                error: function (err) {
                    console.log(err);
                },
                success: function () {
                    followBtn.removeClass("active");
                    followBtn.text(followTxt);
                    followBtn.attr("data-follow-state", "notFollower");
                }
            });
        }
    });

    // get followers 
    $(document).on("click", ".followersCount", function () {
        $.ajax({
            url: "/api/users/" + accountId,
            method: "Put",
            success: function (followers) {

                $(".followersAndfollowing .followContent").empty();

                if (followers.length === 0) {
                    $(".followersAndfollowing .followContent").append(`
                        <div class="alert alert-info">
                            <p class="lead">
                                ${nofollowersMsg}
                            </p>
                        <div>
                    `);

                } else {
                    followers.forEach(function (follower) {
                        $(".followersAndfollowing .followContent").append(`
                            <div class="user overflow">

                                <div class="userImage">
                                    <img src="${follower.profilePicSrc}" alt="${follower.firstName}" />
                                </div>
                                <div class="userName">
                                    <h4>
                                        <a href="/users/account/${follower.id}">${follower.firstName} ${follower.lastName}</a>
                                    </h4>
                                </div>

                            </div>
                        `);
                    });
                }

                $(".followersAndfollowing").fadeIn();
            }
        });
    });

    // get following
    $(document).on("click", ".followingCount", function () {
        $.ajax({
            url: "/api/users/" + accountId,
            method: "Delete",
            success: function (following) {

                $(".followersAndfollowing .followContent").empty();

                if (following.length === 0) {
                    $(".followersAndfollowing .followContent").append(`
                        <div class="alert alert-info">
                            <p class="lead">
                                ${nofollowingMsg}
                            </p>
                        <div>
                    `);

                } else {
                    following.forEach(function (followingUser) {
                        $(".followersAndfollowing .followContent").append(`
                            <div class="user overflow">

                                <div class="userImage">
                                    <img src="${followingUser.profilePicSrc}" alt="${followingUser.firstName}" />
                                </div>
                                <div class="userName">
                                    <h4>
                                        <a href="/users/account/${followingUser.id}">${followingUser.firstName} ${followingUser.lastName}</a>
                                    </h4>
                                </div>

                            </div>
                        `);
                    });
                }

                $(".followersAndfollowing").fadeIn();
            }
        });
    });

    $(".followersAndfollowing .closeIcon").on("click", function () {
        $(this).parents(".followersAndfollowing").fadeOut();  
    });

});