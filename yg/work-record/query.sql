SELECT 
    u.nativeid AS NativeID,
    COALESCE(u.nickname, '') AS Nickname,
    NULLIF(p.descr, '') 'CampaignType',
    u.org AS Brand,
    h.org_group AS Organization,
    bc.name AS CampaignName,
    bc.campaignId AS CampaignID,
    bc.cnwcId,
    tp.wagerid AS TP_WAGER,
    tp.bestScoreDate 'bestScoreDate(UTC)',
    p.wonamount AS WonAmount,
    p.woncurrency AS Currency,
    bcp.prize AS Prize,
    bcp.currency AS PrizeCurrency,
    'sg' AS ENV
FROM
    boost_campaign bc
        LEFT JOIN
    tournament_player tp ON bc.campaignId = tp.tournamentId
        LEFT JOIN
    user u ON tp.userid = u.userid
        LEFT JOIN
    tournament_result tr ON tp.playerId = tr.playerId
        AND tr.hasWon = 1
        LEFT JOIN
    boost_campaign_prize bcp ON tr.prizeId = bcp.campaignPrizeId
        LEFT JOIN
    prize p ON tp.wagerid = p.wagerid
        AND p.descr IN ('NetworkMission' , 'Boost', 'CashRace', 'Tournament')
        JOIN
    (SELECT 
        LOWER(c1.Domain) organization, c2.Domain org_group
    FROM
        util.organizationclosure c1
    JOIN util.organizationclosure c2 ON c1.Parent = c2.Category) h ON LOWER(u.org) = h.organization
WHERE
    bc.campaignid IN (SELECT 
            boostId
        FROM
            game.boost_wager
        WHERE
            wagerid = 1912221315340300006) #change those wagerids 
        AND u.org IN (SELECT 
            oc.wallet
        FROM
            util.organizationclosure oc
                JOIN
            util.organizationclosure oc2 ON oc.parent = oc2.category
                JOIN
            walletcfg wc ON wc.walletid = oc.wallet)
ORDER BY Prize DESC;