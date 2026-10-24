using UnityEngine;

public class Localizer : MonoBehaviour
{
	public enum Language
	{
		English = 0,
		Spanish = 1,
		ChineseSimplified = 2,
		ChineseTraditional = 3,
		Japanese = 4,
		French = 5,
		German = 6,
		Italian = 7,
		Dutch = 8,
		Russian = 9,
		Arabic = 10,
		Korean = 11
	}

	private struct Translatable
	{
		private readonly ArrayUtils.Array<string> translations;

		public Translatable(params string[] TranslatableTranslations)
		{
			translations = new ArrayUtils.Array<string>(TranslatableTranslations);
		}

		public string Get()
		{
			int num = languageIndex.Get();
			if (num < translations.Length)
			{
				return translations[num];
			}
			return GetDefault();
		}

		public string GetInEnglish()
		{
			return translations[0];
		}

		public string GetDefault()
		{
			int num = 0;
			if (num < translations.Length)
			{
				return translations[num];
			}
			return GetInEnglish();
		}
	}

	private const Language defaultLanguage = Language.English;

	private static string[] languageNames = new string[12]
	{
		"English", "Español", "中文 简化字", "中文 簡化字", "日本語", "Français", "Deutsche", "Italiano", "Nederlands", "русский",
		"العربية", "한국어"
	};

	private static Translatable[] terms = new Translatable[51]
	{
		new Translatable("Languages", "Lenguajes", "语言", "語言", "言語", "Langues", "Sprachen", "Lingue", "Talen", "Выбор языка", "اللغات", "언어들"),
		new Translatable("Language", "Lenguaje", "语言", "語言", "言語", "Langue", "Sprache", "Lingua", "Taal", "Язык", "اللغة", "언어"),
		new Translatable("Settings", "Ajustes", "设置", "設置", "設定", "Paramètres", "Einstellungen", "Impostazioni", "Instellingen", "Настройки", "التعديلات", "설정"),
		new Translatable("Music On", "Con Música", "音乐开", "開啟音樂", "音楽オン", "Musique activée", "Musik an", "Musica On", "Muziek aan", "Музыка Вкл", "تشغيل الموسيقى", "음악 켜기"),
		new Translatable("Music Off", "Sin Música", "音乐关", "關閉音樂", "音楽オフ", "Musique désactivée", "Musik Off", "Musica Off", "Muziek uit", "Музыка Выкл", "إيقاف الموسيقى", "음악 끄기"),
		new Translatable("Sounds On", "Con Sonidos", "音效开", "開啟音效", "サウンドオン", "Son activé", "Sounds auf", "Suoni On", "Geluiden aan", "Звук Вкл", "تشغيل الصوت", "소리 켜기"),
		new Translatable("Sounds Off", "Sin Sonidos", "音效关", "關閉音效", "サウンドオフ", "Son désactivé", "Sounds Off", "Suoni Off", "Geluiden uit", "Звук Выкл", "إيقاف الصوت", "소리 끄기"),
		new Translatable("Alerts On", "Con Alertas", "警报开", "開啟提示", "アラートオン", "Alerte activée", "Warnungen auf", "Avvisi On", "Waarschuwingen", "Уведомления", "تشغيل التنبيه", "경고음 켜기"),
		new Translatable("Alerts Off", "Sin Alertas", "警报关", "關閉提示", "アラートオフ", "Alerte désactivée", "Alarme deaktivieren", "Avvisi Off", "Waarschuwingen", "Уведомления", "إيقاف التنبيه", "경고음 끄기"),
		new Translatable("Got It", "Entendido", "明白了", "知道了", "分かりました", "D'accord", "OK", "Preso", "Gesnapt", "Хорошо", "حصلت عليه", "알겠습니다"),
		new Translatable("OK", "OK", "确认", "確認", "了解", "OK", "OK", "OK", "OK", "ОК", "حسنا", "네"),
		new Translatable("No thanks", "No, Gracias", "不，谢谢", "不用了，謝謝", "結構です", "Non merci", "Nein", "No Grazie", "Nee bedankt", "Нет, спасибо", "لا شكرا", "고맙지만 됐어요"),
		new Translatable("Not Ready", "No esta listo", "未准备好", "沒有準備好", "まだ結構です", "Pas encore prêt", "Nicht bereit", "Ancora No", "Niet klaar", "Не готов", "لست مستعدا", "아직 준비되지 않았어요"),
		new Translatable("Upgrade", "Mejorado", "升级", "升級", "アップグレード", "Mettre à niveau", "Kauf", "Aggiornamento", "Upgrade", "Улучшить", "تطور", "업그레이드"),
		new Translatable("Notifications", "Notificaciones", "提醒", "提示", "通知", "Notifications", "Benachrichtigungen", "Notifiche", "Meldingen", "Уведомления", "الإشارات", "알림"),
		new Translatable("Price", "Precio", "价格", "價格", "価格", "Prix", "Preis", "Prezzo", "Prijs", "Цена", "السعر", "가격"),
		new Translatable("Last", "Último", "最近", "上一次", "前回", "Dernier", "Letzte", "Ultimo", "Laatst", "Последний", "الأخير", "마지막"),
		new Translatable("Best", "Mejor", "最佳", "最佳", "最高", "Meilleur", "beste", "Migliore", "Best", "Лучший", "الأفضل", "최고"),
		new Translatable("Complete", "Completo", "完成", "完成", "完了", "Complet", "Komplett", "Completo", "Volslagen", "Завершен", "الكامل", "완료"),
		new Translatable("balls", "bolas", "球", "球", "ボール", "Balles", "Bälle", "palle", "ballen", "Шары", "الكرات", "공들"),
		new Translatable("balls remaining", "bolas restantes", "剩余的球", "剩下的球", "残りボール数", "Balles restantes", "Rest Ball", "palla rimanente", "ballen verblijvend", "шаров осталось", "الكرات التي مازالت", "남은 공들"),
		new Translatable("ball remaining!", "Bola restante", "剩余的球", "剩下的球 ", "残りボール数", "Balles restantes", "Rest Ball!", "palla remasta", "bal verblijvend", "шар остался", "الكرة التي مازالت", "남은 공"),
		new Translatable("out of balls", "No quedan bolas", "球已经用完", "球不夠", "ボールなし", "Plus de balles", "von Kugeln", "palle finite", "geen ballen meer", "закончились шары", "انتهت الكرات ", "어떤 공 없습니다"),
		new Translatable("get balls", "Consigue más bolas", "获得球", "索取球", "ボールを獲得", "Obtenir des balles", "zu Kugeln", "gttenere altre palle", "haal ballen", "получить шары", "تحصل على كرات ", "공 습득"),
		new Translatable("more", "más", "更多", "更多", "その他", "fuite", "leckage", "altre", "meer", "еще", "أكثر", "더"),
		new Translatable("Recharge in:", "Recarga en: ", "填充 ", "補充 ", "ボール補充間隔 ", "Recharge dans ", "Aufladen ", "Caricamento In ", "opladen in ", "Перезарядка через ", "إعادة الشحن", "다시 채우기 "),
		new Translatable("minutes", "minutos", "分钟", "分鐘", "分", "minutes", "Minuten", "minuti", "minuten", "минут", "الدقائق", "분"),
		new Translatable("seconds", "segundos", "秒", "秒", "秒", "secondes", "Sekunden", "secondi", "seconden", "секунд", "الثواني", "초"),
		new Translatable("Done", "Listo", "完成", "完成", "完了", "Terminé", "Fertig", "Finito", "Afgewerkt", "Готово", "تم\u0651 الأمر", "완료"),
		new Translatable("Unlimited", "Ilimitado", "无限量的", "無限", "無制限", "Illimité", "Unbegrenzt", "Illimitato", "Onbeperkt", "Бесконечно", "غير محدود", "무제한"),
		new Translatable("free", "gratis", "免费的", "免費", "無料", "gratuit", "frei", "gratuito", "gratis", "бесплатно", "حر\u0651", "무료"),
		new Translatable("or", "O", "或者", "或", "または", "ou", "oder", "o", "of", "или", "أو", "혹은"),
		new Translatable("Reading", "Leyendo", "读入中", "讀取中", "取得中", "Chargement", "Laden", "Leggendo", "Leest", "Чтение", "بصدد القراءة", "읽는중입니다"),
		new Translatable("Loading", "Cargando", "载入中", "載入中", "読み込み中", "Configuration", "Konfigurieren", "Caricando", "Laadt", "Загрузка", "انتظر قليلا", "로딩중입니다"),
		new Translatable("Configuring", "Configurando", "配置中", "配置", "設定中", "Chargement", "Laden", "Configurando", "Configureert", "Конфигурация", "تكوين ", "수정중입니다"),
		new Translatable("Creating", "Creando", "生成中", "創建", "作成中", "Configuration", "Konfigurieren", "Creando", "Scheept", "Создание", "إنشاء", "생성중입니다"),
		new Translatable("No Internet", "Sin Internet", "没有网络", "沒有連線", "インターネット接続がありません。", "Pas d'Internet", "Kein Internet", "No Internet", "Geen internet", "Нет подключения", "لا توجد انترنيت ", "인터넷에 연결되어있지 않습니다"),
		new Translatable("Purchase Failed", "Compra fallida", "购买失败", "訂單失敗", "購入に失敗しました。", "Échec de l'achat", "Kauf fehlgeschlagen", "Acquisto Fallito", "Inkoop mislukt", "Покупка не удалась", "الشراء قد أخفق", "구매 실패"),
		new Translatable("Purchase attempt failed", "Intento de compra fallida", "尝试购买失败", "訂單嘗試失敗", "購入エラーが発生しました。", "Échec de la tentative d'achat", "Kaufversuch fehlgeschlagen", "Tentativo di acquisto fallito", "Inkoop aanslag mislukt", "Попытка покупки не удалась", "محاولة الشراء أخفقت ", "구매 시도 실패"),
		new Translatable("Purchase cancelled", "Compra Cancelada", "已取消购买", "訂單取消", "購入が中止されました。", "Achat annulé", "Einkauf abgebrochen", "Acquisto cancellato", "Inkoop geannuleerd", "Покупка отменена", "الشراء قد سحب", "구매 취소"),
		new Translatable("Restore", "Restaure", "重新执行", "回復", "リストア", "Restaurer", "Wiederherstellen", "Ripristinare", "Herstellen", "Восстановить", "أعد العملية", "되돌리기"),
		new Translatable("Restore Completed", "Restauracion Completada", "重新执行完成", "回復完成", "リストアが完了しました。", "Restauration effectuée", "Wiederherstellung abgeschlossen", "Ripristino completato", "Herstel uitgevoerd", "Восстановление завершено", "الإعادة أكملت تماما ", "완료된것을 되돌립니다"),
		new Translatable("Restore Failed", "Restauracion Fallida", "重新执行失败", "回復失敗", "リストア", "Échec de la restauration", "Wiederherstellung fehlgeschlagen", "Ripristino Fallito", "Herstel mislukt", "Восстановление не удалось", "الإعادة فشلت ", "실패한 것을 되돌립니다"),
		new Translatable("Restore attempt failed", "Intento de restauración fallida", "尝试重新执行失败", "回復嘗試失敗", "リストアに失敗しました。", "Échec de la tentative de restauration", "Wiederherstellungsversuch fehlgeschlagen", "Tentativo di ripristino fallito", "Herstel aanslag mislukt", "Попытка восстановления не удалась", "محاولة الإعادة فشلت ", "시도 실패를 되돌립니다"),
		new Translatable("Paused", "Pausa", "暂停", "暫停", "一時停止", "Tenir", "Halten", "tenere", "Greep", "Пауза", "وقفة", "중지"),
		new Translatable("Tip", "Indicio", "暗示", "暗示", "ヒント", "Allusion", "Tipp", "Spunto", "Wenk", "намек", "تلميح", "힌트"),
		new Translatable("Everyplay"),
		new Translatable("."),
		new Translatable("Spooling", "Cargando", "载入中", "載入中", "荷積", "Chargement", "Laden", "Caricamento", "het laden", "Настройка", "تحميل", "로드"),
		new Translatable("Extending", "Configuración", "配置", "配置", "設定", "Configuration", "Konfigurieren", "Configurazione", "configureren", "загрузка", "تكوين", "구성"),
		new Translatable("Grooving", "Cargando", "载入中", "載入中", "荷積", "Chargement", "Laden", "Caricamento", "het laden", "Настройка", "تحميل", "구성")
	};

	private static Translatable[] lines = new Translatable[19]
	{
		new Translatable("You must be connected to the internet to ", "Debe estar conectado al internet para ", "你必须连接到网络以 ", "您必須連接到互聯網 ", "以下を行なうためにはインターネットへの接続が必要です：", "Vous devez être connecté à internet pour ", "Internet an erforderlichen ", "Deve essere collegato a internet per ", "Je moet met het internet verbonden zijn, om ", "Интернет требуется ", "يجب أن تكون موصولا بالانترنيت", "인터넷에 연결되어야만 합니다 "),
		new Translatable("watch a video for free balls", "Mire un video y consiga mas bolas", "观看一个视频获得免费球", "觀看免費球的視頻", "動画を見て無料分のボールを獲得", "Regardez une vidéo pour avoir des balles gratuitement", "ein Video kostenlos Kugeln", "Guarda un video per ricevere palle gratis", "een video voor gratis ballen te kijken", "посмотреть видео за бесплатные шары", "شاهد فيديو للكرات الحر\u0651ة", "무료 공을 얻기 위해 동영상 감상"),
		new Translatable("restore previous purchases", "Restaurer les achats précédents", "重新执行以前的购买", "回復之前的訂單", "以前の購入をリストア", "restaurer achats précédents", "Wiederherstellen früherer Einkäufe", "Ripristinare gli acquisti precedenti", "herstel vorige inkopen", "восстановить предыдущие покупки", "أعد الشراءات القديمة", "지난 구매 되돌리기 "),
		new Translatable("access achievements", "Acceso a logros", "获得成就", "觀看成就", "成果にアクセス", "Voir les prouesses réalisées", "Zugang Erfolge", "Accedi I realizzazioni", "bereik prestaties", "открыть достижения", "اطلع على الإنجازات", "액세스 성과"),
		new Translatable("make purchases", "Haga Compras", "采购", "下訂單", "購入を実施", "Faire des achats", "Einkäufe machen", "fare acquisti", "maak inkopen", "совершить покупки", "قم بالشراء", "구매하기"),
		new Translatable("rate game", "Denos su rating", "评价游戏", "評價遊戲", "ゲームを評価", "Noter le jeu", "Latte Aquarius", "Tasso di gioco", "beoordeel het spel", "оценить игру", "قي\u0651م اللعبة", "게임 평가하기"),
		new Translatable("This will turn off ball recharge notification", "Esto apagará la notificación de compra de bolas", "这将关闭装填球的提醒", "這將關閉補充球時的通知", "ボール補充通知がオフになります。", "Ceci désactivera les notifications de recharge de balle", "Damit schaltet sich Anruflast Benachrichtigung", "Questo spingera le notifiche del caricamento delle palle", "Dat zal de melding voor bal opladingen uitzetten", "Это отключит уведомление о перезарядке шаров", "هذا سيوقف عمل كل مؤشرات تعبئة الكرات", "공의 재충전 알림을 끕니다"),
		new Translatable("Use this to restore all previous purchases", "Utilize esto para restaurar todas las compras anteriores", "使用这个重新执行所有以前的购买", "在此回復所有以前的訂單", "今までの全購入履歴を復元することができます。", "Ceci restaurera tous les achats précédents", "Use this to restore all previous purchases", "Usa questo per ripristinare gli acquisti precedenti", "Gebruik dit om alle vorige inkopen te herstellen", "Нажмите, чтобы восстановить все предыдущие покупки", "استعمل هذا لإعادة جميع العمليات الشرائية", "모든 이전 구매를 되돌리기 위해 이것을 사용합니다"),
		new Translatable("Pressing this button will record your next game for sharing online", "Pulsando esto se grabara su próximo juego para ser compratido en linea.", "按下这个按键记录你的下一次游戏以在线分享", "按此會記錄您的下一場遊戲來在網上共享。", "このボタンを押せば、次のゲームを記録し、オンラインで共有することができます。", "En appuyant vous enregistrez votre prochaine partie pour le partage en ligne", "Durch Drücken dieser Taste wird Ihr Spiel für die gemeinsame Nutzung aufzeichnen.", "Compartecipazione Online.", "Als je dit knopje drukt zal jouw volgend spel worden opgenomen voor het online delen.", "Включите, чтобы записать вашу следующую игру и поделиться записью онлайн", "النقر على هذا الزر سيمكنك من تسجيل لعبتك الجديدة لكي تشاركها على الخط\u0651", "온라인에 공유합니다"),
		new Translatable("Sadly a video is not yet cached and ready for you - just a moment", "Video no disponible, por favor espere", "视频不可用，请稍候", "視頻不可用，請稍候", "動画が利用できません。少々お待ち下さい。", "La vidéo n'est pas disponible, veuillez patienter", "Belohnt Videos nicht bereit, bitte warten.", "Il video non e disponibile, attendere prego.", "Video is niet beschikbaar, wacht a.u.b.", "Видео недоступно, подождите пожалуйста.", "الفيديو ليس جاهزا, انتظر من فضلك", "영상을 볼 수 없습니다, 기다려주세요"),
		new Translatable("Would you like to upgrade to the full version for unlimited balls?", "Cambie a la versión completa para tener bolas ilimitadas?", "升级到完全版获得无限量的球？", "升級到有無限球的完整版本嗎？", "フルバージョンまたは無制限ボールへとアップグレードしますか？", "Obtenir la version complète pour avec des balles illimitées ?", "Möchten Sie auf die Vollversion für unbegrenzte Bälle aktualisieren?", "Aggiornamento alla versione completa per le palle illimitato?", "Upgrade naar volle versie voor onbeperkte ballen?", "Перейти на полную версию и получить бесконечные шары?", "تطور إلى الإصدار الكامل: عدد غير محدود من الكرات؟", "무제한 공을 위해 풀버전으로 업그레이드 하시겠습니까?"),
		new Translatable("Would you like to be automatically notified when your balls have recharged?", "Quisiera ser notificado cuando las bolas hayan sido recargadas?", "你想在球填充完成时收到提醒吗？", "你是否想得到補充球的通知？", "ボールが補充された時に通知を受け取りますか？", "Souhaitez-vous être notifié quand les balles ont été rechargées ?", "Möchten Sie automatisch benachrichtigt, wenn Ihre Bälle haben wieder aufgeladen werden?", "Vuoi essere avvisato automaticamente quando le palle sono ricaricato?", "Wil je graag meldingen ontvangen als ballen zijn opgeladen?", "Вы бы хотели получать уведомления о перезагрузке шаров?", "هل تريد أن ننبهك إذا تم شحن الكرات؟", "충전하겠습니까?"),
		new Translatable("Your balls have recharged and are ready to be used", "SLas bolas han sido recargadas y esta listas para su uso", "球已填充完成并做好使用准备", "球已補充好並準備使用", "ボールが補充されました。使用準備完了です。", "VLes balles ont été rechargées et sont prêtes à être utilisées", "Ihre Bälle haben wieder aufgeladen und bereit sind, verwendet werden,", "Le palle sono ricaricate e sono pronte per essere usate", "Ballen hebben opgeladen en zijn klaar om gebruikt te worden", "Шары перезагружены и готовы к использованию", "تم شحن الكرات, يمكنك البدء الآن", "충전한 공을 사용하실 수 있습니다"),
		new Translatable("Turns off recharge notification", "Apagar notificaciones de recarga", "关闭填充提醒", "關閉補充的通知", "補充通知をオフにします。", "Désactiver les notifications de recharge", "Ihre Bälle haben wieder aufgeladen und bereit sind, verwendet werden,", "Spingere le notifiche del caricamento", "Zet uit meldingen van opladingen", "Выключает уведомление о перезагрузке", "إيقاف تشغيل شحن التأشيرات", "충전 알림 끄기"),
		new Translatable("You must sign in to Game Center to access leaderboards.", "No sesión en Game Center", "没有登录到游戏中心", "沒有登錄到遊戲中心", "れていないゲームセンターにサインイン", "Non signé pour Game Center", "Nicht in den Game Center unterzeichnet", "Non l'accesso a Game Center", "Niet aangemeld bij Game Center", "Не вошли в Game Center", "Game Center تسجيل الدخول إلى", "에 로그인 Game Center"),
		new Translatable("You must sign in to Google Play Services to access leaderboards.", "No sesión en Game Center", "没有登录到游戏中心", "沒有登錄到遊戲中心", "れていないゲームセンターにサインイン", "Non signé pour Google Play", "Nicht in den Google Play unterzeichnet", "Non l'accesso a Google Play", "Niet aangemeld bij Google Play", "Не вошли в Google Play", "Google Play تسجيل الدخول إلى", "에 로그인 Google Play"),
		new Translatable("Slide THUMB below ball\nfor better control", "Mueva el dedo por debajo de la \nbola para un mejor control", "移动手指的下\n方球最好控制", "移動手指的\n下方球最好控制", "最高の制御のためにボー\nルの下に指を移動します", "Déplacez doigt ci-dessous balle\npour un meilleur contrôle", "Bewegen Sie den Finger unter\nBall für beste Kontrolle", "Spostare un dito sotto palla\n per un miglior controllo", "Beweeg vinger hieronder\n bal voor de beste controle", "Перемещение пальцем ниже\nмяч для лучшего контроля", "التحرك دون كر\nة لسيطرة أفضل", "최적의 제어를 위해 볼\n 아래에 손가락을 이동"),
		new Translatable("Slide finger below ball\nfor better control", "Mueva el dedo por debajo de la \nbola para un mejor control", "移动手指的下\n方球最好控制", "移動手指的\n下方球最好控制", "最高の制御のためにボー\nルの下に指を移動します", "Déplacez doigt ci-dessous balle\npour un meilleur contrôle", "Bewegen Sie den Finger unter Ball \nfür beste Kontrolle", "Spostare un dito sotto palla\n per un miglior controllo", "Beweeg vinger hieronder\n bal voor de beste controle", "Перемещение пальцем ниже\nмяч для лучшего контроля", "التحرك دون كر\nة لسيطرة أفضل", "최적의 제어를 위해 볼\n 아래에 손가락을 이동"),
		new Translatable("You have previously purchased Upgrade. Upgrade restored.", "Usted ha comprado previamente actualización. Actualiza restaurado.", "您先前已经购买了升级。升级恢复。", "您先前已經購買了升級。升級恢復。", "あなたは以前のアップグレードを購入しています。アップグレード復元。", "Vous avez déjà acheté la mise à niveau. Améliorez restauré.", "Sie haben bereits gekaufte aktualisieren. Upgrade wiederhergestellt.", "Avete già acquistato l'aggiornamento. Aggiornamento restaurato.", "Je hebt eerder gekochte Upgrade. Upgraden hersteld.", "Вы уже приобрели Upgrade. Обновление восстановлена.", "لقد تم شراؤها من قبل الترقية. ترقية استعادتها.", "당신은 이전에 업그레이드를 구입했습니다. 업그레이드 복원.")
	};

	private static Translatable[] numbers = new Translatable[20]
	{
		new Translatable("1"),
		new Translatable("2"),
		new Translatable("3"),
		new Translatable("4"),
		new Translatable("5"),
		new Translatable("6"),
		new Translatable("7"),
		new Translatable("8"),
		new Translatable("9"),
		new Translatable("10"),
		new Translatable("11"),
		new Translatable("12"),
		new Translatable("13"),
		new Translatable("14"),
		new Translatable("15"),
		new Translatable("16"),
		new Translatable("17"),
		new Translatable("18"),
		new Translatable("19"),
		new Translatable("20")
	};

	private static Persistant.Int languageIndex = new Persistant.Int("Chosen Language", "Chosen Language (index)", 0);

	private static Language DeviceSystemLanguageToLanguage()
	{
		Language result = Language.English;
		switch (Application.systemLanguage)
		{
		case SystemLanguage.Spanish:
			result = Language.Spanish;
			break;
		case SystemLanguage.Chinese:
			result = Language.ChineseSimplified;
			break;
		case SystemLanguage.Japanese:
			result = Language.Japanese;
			break;
		case SystemLanguage.French:
			result = Language.French;
			break;
		case SystemLanguage.German:
			result = Language.German;
			break;
		case SystemLanguage.Italian:
			result = Language.Italian;
			break;
		case SystemLanguage.Dutch:
			result = Language.Dutch;
			break;
		case SystemLanguage.Russian:
			result = Language.Russian;
			break;
		case SystemLanguage.Arabic:
			result = Language.Arabic;
			break;
		case SystemLanguage.Korean:
			result = Language.Korean;
			break;
		}
		return result;
	}

	public static void SetDefaultSystemLanguageFirstLaunch()
	{
		if (!PlayerPrefs.HasKey("l_idlc"))
		{
			PlayerPrefs.SetInt("l_idlc", 1);
			PlayerPrefs.Save();
			SetLanguageTo(DeviceSystemLanguageToLanguage());
		}
	}

	public static void SetLanguageTo(Language chosenLanguage)
	{
		SetLanguageTo((int)chosenLanguage);
	}

	public static void SetLanguageTo(int chosenLanguageIndex)
	{
		languageIndex.Set(chosenLanguageIndex);
	}

	public static void NextLanguage()
	{
		int num = languageIndex.Get();
		int num2 = num + 1;
		int num3 = EnumUtils.MaxEnumAsInt<Language>();
		if (num2 > num3)
		{
			num2 = 0;
		}
		languageIndex.Set(num2);
	}

	public static string GetTerm(string englishTerm)
	{
		bool couldTranslate;
		return GetTranslated(englishTerm, terms, out couldTranslate);
	}

	public static string GetLine(string englishLine)
	{
		bool couldTranslate;
		return GetTranslated(englishLine, lines, out couldTranslate);
	}

	public static string GetNumber(int englishNumber)
	{
		bool couldTranslate;
		return GetTranslated(englishNumber.ToString(), numbers, out couldTranslate);
	}

	public static string GetTermCapitalized(string englishTerm)
	{
		return GetTerm(englishTerm).ToUpper();
	}

	public static string GetLineCapitalized(string englishLine)
	{
		return GetLine(englishLine).ToUpper();
	}

	public static string GetCurrentLanguage()
	{
		return ((Language)languageIndex.Get()).ToString();
	}

	public static Language GetCurrentLanguageEnum()
	{
		return (Language)languageIndex.Get();
	}

	public static string GetCurrentLanguageCapitalized()
	{
		return GetCurrentLanguage().ToUpper();
	}

	public static string[] GetLanguages()
	{
		return languageNames;
	}

	public static string[] GetLanguagesCapitalized()
	{
		string[] array = new string[languageNames.Length];
		for (int i = 0; i < languageNames.Length; i++)
		{
			array[i] = languageNames[i].ToUpper();
		}
		return array;
	}

	private static string GetTranslated(string englishTranslatable, Translatable[] searchTranslatables, out bool couldTranslate)
	{
		string result = null;
		if (languageIndex.Get() == 0)
		{
			result = englishTranslatable;
			couldTranslate = true;
		}
		else if (string.IsNullOrEmpty(englishTranslatable))
		{
			Debug.LogError("Localizer: ERROR: Parsed NULL string for Localizer.Get() - unable to translate");
			couldTranslate = false;
		}
		else
		{
			couldTranslate = false;
			for (int i = 0; i < searchTranslatables.Length; i++)
			{
				if (string.Equals(englishTranslatable, searchTranslatables[i].GetInEnglish()))
				{
					couldTranslate = true;
					result = searchTranslatables[i].Get();
					break;
				}
			}
			if (!couldTranslate)
			{
				string a = englishTranslatable.ToLower();
				for (int j = 0; j < searchTranslatables.Length; j++)
				{
					if (string.Equals(a, searchTranslatables[j].GetInEnglish().ToLower()))
					{
						couldTranslate = true;
						result = searchTranslatables[j].Get();
						break;
					}
				}
			}
			if (!couldTranslate)
			{
				Debug.LogWarning(string.Format("Localizer: Warning: Was not able to find the english Translatable: '{0}' in Localizer's stored Translatables. Please add '{0}' to Localizer.Translatables", englishTranslatable));
				result = englishTranslatable;
			}
		}
		return result;
	}
}
