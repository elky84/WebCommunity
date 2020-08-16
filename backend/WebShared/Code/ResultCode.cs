using WebShared.Common;

namespace Web.Code
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum ResultCode
    {
        [Description("성공")]
        Success,

        [Description("실패")]
        Fail,

        [Description("Http오류")]
        HttpError,

        [Description("잘못된 요청")]
        BadRequest,

        [Description("프로토콜이 맞지 않습니다")]
        ProtocolNotMatched,

        [Description("파라미터 검증 실패")]
        ValidationFailed,

        [Description("유효하지 않은 토큰")]
        InvalidToken,

        [Description("토큰이 전달되지 않았다")]
        NotPrivodedToken,

        [Description("DSL 오류")]
        DslException,

        [Description("계정을 찾지 못했다")]
        NotFoundAccount,

        [Description("UserId가 규칙에 위배되었다")]
        InvalidUserId,

        [Description("핸들링하지 못하는 예외가 발생했다")]
        UnknownException,

        [Description("스테이지 데이터를 찾지 못함")]
        NotFoundStageData,

        [Description("이미 스테이지 데이터가 존재한다")]
        AlreadyExistStageData,

        [Description("허용되지 않은 챕터")]
        NotAllowChapterId,

        [Description("허용되지 않은 스테이지")]
        NotAllowStageId,

        [Description("진행 중인 스테이지 아이디와 일치하지 않는다")]
        NotMatchedPlayingStageId,

        [Description("진행 중인 게임 정보와 일치하지 않는다")]
        NotMatchedPlayingGameId,

        [Description("게임 정보를 찾을 수 없다")]
        NotFoundGameId,

        [Description("MQ와 연결이 되지 않았다")]
        NotConnectedMQ,

        [Description("스테이지 진행 정보를 찾을 수 없다")]
        NotFoundStage,

        [Description("사용 불가능 한 계정이다")]
        NotUsableAccount,

        [Description("서버로부터 연결해제 되었다")]
        DisconnectByServer,

        [Description("이미 인증을 받은 상태이다")]
        AlreadyAuth,

        [Description("해당 레벨의 부스터 데이터를 찾지 못했다")]
        NotFoundBooster,

        [Description("해당 레벨의 부스터 룰렛 데이터를 찾지 못했다")]
        NotFoundBoosterRouletteByLevel,

        [Description("부스터 룰렛 최대 시도 횟수를 초과했습니다.")]
        OverflowBoosterRouletteTryCount,

        [Description("부스터 룰렛을 다시 굴리는 것을 허용하지 않는다. (이미 성공)")]
        BoosterRouletteCannotPermitReRoll,

        [Description("부스터 룰렛이 실패한 상태에서, 다음 단계로 진행하려 했습니다")]
        BoosterRouletteCannotPermitNextLevel,

        [Description("다음 단계 부스터 룰렛이 없어, 룰렛을 돌릴 수 없습니다")]
        NotFoundNextLevelBoosterRoulette,

        [Description("부스터 단계가 유효하지 않습니다.")]
        InvalidBoosterLevel,

        [Description("인증 서버 라우팅 정보를 못찾았다")]
        NotFoundAuthRoutes,

        [Description("아직 구현되지 않았다")]
        NotImplementedYet,

        [Description("재화 정보를 찾지 못함")]
        NotFoundCurrencyData,

        [Description("골드가 모자르다")]
        NotEnoughGold,

        [Description("전투식량이 모자르다")]
        NotEnoughBattleFood,

        [Description("다이아몬드가 모자르다")]
        NotEnoughDiamond,

        [Description("무료 다이아몬드가 모자르다")]
        NotEnoughFreeDiamond,

        [Description("유료 다이아몬드가 모자르다")]
        NotEnoughCashDiamond,

        [Description("스킬 덱 Id를 찾지 못함")]
        NotFoundSkillDeckId,

        [Description("스킬 덱 정보를 못 찾음")]
        NotFoundSkillDeckData,

        [Description("캐릭터 정보를 못 찾음")]
        NotFoundCharacterData,

        [Description("카드 정보를 못 찾음")]
        NotFoundCardData,

        [Description("스킬 정보를 못 찾음")]
        NotFoundSkillData,

        [Description("스킬 슬롯 정보를 못 찾음")]
        NotFoundSkillSlotData,

        [Description("아이템 마스터 데이터 정보를 못 찾음")]
        NotFoundItemCase,

        [Description("아이템 정보를 못 찾음")]
        NotFoundItemData,

        [Description("분해할 아이템을 찾지 못함")]
        NotFoundDisassembleItemData,

        [Description("아이템 슬롯 정보를 못 찾음")]
        NotFoundItemSlotData,

        [Description("장착 옵션 마스터 데이터 정보를 못 찾음")]
        NotFoundItemEquipOption,

        [Description("장착 마스터 데이터 정보를 못 찾음")]
        NotFoundItemEquipment,

        [Description("장착 Lv 마스터 데이터 정보를 못 찾음")]
        NotFoundItemEquipLv,

        [Description("장착 등급 마스터 데이터 정보를 못 찾음")]
        NotFoundItemEquipGrade,

        [Description("장착 캐릭터 마스터 데이터 정보를 못 찾음")]
        NotFoundItemEquipCharacter,

        [Description("옵션 스탯 마스터 데이터 정보를 못 찾음")]
        NotFoundItemOptionStat,

        [Description("스테이지 마스터 데이터 정보를 못 찾음")]
        NotFoundStageMasterData,

        [Description("챕터 마스터 데이터 정보를 못 찾음")]
        NotFoundChapterMasterData,

        [Description("보상 확률 마스터 데이터 정보를 못 찾음")]
        NotFoundRewardProbabilityMasterData,

        [Description("보상 등급 마스터 데이터 정보를 못 찾음")]
        NotFoundRewardGradeMasterData,

        [Description("보상 정보를 못 찾음")]
        NotFoundRewardData,

        [Description("재료로 소모할 재료 정보를 못 찾음")]
        NotFoundConsumeMaterial,

        [Description("재료 정보를 못 찾음")]
        NotFoundConsumeItem,

        [Description("조건에 맞는 아이템 마스터 데이터를 찾지 못했음")]
        NotFoundMatchedItemMasterData,

        [Description("조건에 맞는 스킬 마스터 데이터를 찾지 못했음")]
        NotFoundMatchedSkillMasterData,

        [Description("스택 아이템 갯수가 부족함")]
        NotEnoughStackItem,

        [Description("장착 아이템 갯수가 부족함.")]
        NotEnoughEquipItem,

        [Description("스킬이 부족함")]
        NotEnoughSkill,

        [Description("장착 아이템이 아니다")]
        NotEquipItem,

        [Description("강화 마스터 데이터 정보를 못 찾음")]
        NotFoundForgeEnhance,

        [Description("분해 마스터 데이터 정보를 못 찾음")]
        NotFoundForgeDisassemble,

        [Description("제작 마스터 데이터 정보를 못 찾음")]
        NotFoundForgeCraft,

        [Description("보상 가능 인원수보다, 더 많은 보상 인원을 요청했음")]
        OverSelectRewardCharacter,

        [Description("캐릭터를 보유하고 있지 않다")]
        NotHaveCharacters,

        [Description("장착 슬롯이 이미 사용중이다")]
        EquipSlotIsAlreadyInuse,

        [Description("해당 캐릭터가 장착 할 수 없는 장비입니다")]
        CharacterIsCannotEquipped,

        [Description("슬롯이 개방되어 있지 않다")]
        EquipSlotIsNotOpened,

        [Description("이미 장착중인 카드다")]
        AlreadyEquipCard,

        [Description("슬롯에 장착되지 않은 카드다")]
        NoEquippedCardOnEquipSlot,

        [Description("슬롯에 장착되지 않은 스킬이다")]
        NoEquippedSkillOnEquipSlot,

        [Description("장착아이템이 이미 사용중이다")]
        EquipItemIsAlreadyInUse,

        [Description("슬롯에 장착되지 않은 아이템이다")]
        NoEquippedItemOnEquipSlot,

        [Description("슬롯에 장착되지 않은 보석이다")]
        NoEquippedGemOnEquipSlot,

        [Description("스테이지 보상을 줄 수 있는 상태가 아닙니다.")]
        StageRewardStateIsNoWait,

        [Description("캐릭터 업그레이드를 위해선 최대 레벨이어야 합니다")]
        CharacterUpgradeNeedMaxLevel,

        [Description("강화 실패")]
        EnhanceFailed,

        [Description("경험치 옵션 아이템이 아닙니다")]
        NotExpOptionItem,

        [Description("장착 아이템은 소모 불가합니다")]
        ConsumeItemMustNotEquipped,

        [Description("아이템을 판매할 수 없습니다")]
        CannotSellItem,

        [Description("스테이지 보상을 위해선 추가 시간이 필요합니다")]
        StageRewardIsNeedMoreTime,

        [Description("스테이지 시작 대기 시간 중입니다.")]
        StageStartIsNeedMoreTime,

        [Description("채팅 채널 최대 인원을 넘어섰습니다.")]
        OverflowChatChannel,

        [Description("스킬 경험치 데이터를 찾지 못했다")]
        NotFoundSkillExpData,

        [Description("장착중인 아이템은 판매 불가하다")]
        NotSellEquippedItem,

        [Description("캐릭터 스탯 정보를 못찾음")]
        NotFoundCharacterStatData,

        [Description("순환 상품 마스터 데이터를 찾지 못했습니다.")]
        NotFoundRotationProductMasterData,

        [Description("순환 상품 정보를 찾지 못했습니다. (로테이션에따라 바뀌었을 수 있음)")]
        NotFoundRotationProductData,

        [Description("제작 대상 목록에 없는 아이템입니다.")]
        NotFoundIdFromCraftableIdList,

        [Description("유효하지 않은 보상 정보")]
        InvalidRewardValue,

        [Description("상품 정보를 찾지 못했다")]
        NotFoundPreviewProductData,

        [Description("이미 사용되었습니다")]
        AlreadyUsedPreviewProductData,
    }
}
