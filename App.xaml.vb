''' <summary>
''' 提供應用程式專屬行為以補充預設的應用程式類別。
''' </summary>
NotInheritable Class App
    Inherits Application

    ''' <summary>
    ''' 在應用程式由終端使用者正常啟動時叫用。當啟動應用
    ''' 程式來開啟特定檔案時，將使用其他進入點來顯示
    '''搜尋結果等資訊。
    ''' </summary>
    ''' <param name="e">關於啟動要求和處理序的詳細資料。</param>
    Protected Overrides Sub OnLaunched(e As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)
        Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)

        ' 當視窗中已有內容時，不重複應用程式初始化，
        ' 只確定視窗是作用中

        If rootFrame Is Nothing Then
            ' 建立框架做為巡覽內容，並巡覽至第一頁
            rootFrame = New Frame()

            AddHandler rootFrame.NavigationFailed, AddressOf OnNavigationFailed

            If e.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                ' TODO: 從之前暫停的應用程式載入狀態
            End If
            ' 將框架放在目前視窗中
            Window.Current.Content = rootFrame
        End If

        If e.PrelaunchActivated = False Then
            If rootFrame.Content Is Nothing Then
                ' 在巡覽堆疊未還原時，巡覽至第一頁，
                ' 設定新的頁面，方式是透過傳遞必要資訊做為巡覽
                ' 參數
                rootFrame.Navigate(GetType(MainPage), e.Arguments)
            End If

            ' 確定目前視窗是作用中
            Window.Current.Activate()
        End If
    End Sub

    ''' <summary>
    ''' 在巡覽至某頁面失敗時叫用
    ''' </summary>
    ''' <param name="sender">導致巡覽失敗的框架</param>
    ''' <param name="e">有關巡覽失敗的詳細資料</param>
    Private Sub OnNavigationFailed(sender As Object, e As NavigationFailedEventArgs)
        Throw New Exception("Failed to load Page " + e.SourcePageType.FullName)
    End Sub

    ''' <summary>
    ''' 在應用程式暫停執行時叫用。應用程式狀態會儲存起來，
    ''' 但不知道應用程式即將結束或繼續，而且仍將記憶體
    ''' 的內容保持不變。
    ''' </summary>
    ''' <param name="sender">暫停之要求的來源。</param>
    ''' <param name="e">有關暫停之要求的詳細資料。</param>
    Private Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()
        ' TODO: 儲存應用程式狀態，並停止任何背景活動
        deferral.Complete()
    End Sub

End Class
